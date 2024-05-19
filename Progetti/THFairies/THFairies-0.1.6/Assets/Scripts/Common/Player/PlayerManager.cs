using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    private static PlayerManager _singleton;
    public static PlayerManager Singleton { get { return _singleton;}}
    private static Dictionary<ulong, int> _playersIndexes;
    public static Dictionary<ulong, int> playersIndexes{get{return _playersIndexes;}}
    private static List<Player> _players;
    public static Player[] players { get {return _players.ToArray();}}
    private static Player _localPlayer;
    public static Player localPlayer {get{return _localPlayer;}}
    [SerializeField] Player playerPrefab;
    void Awake(){
        _singleton = this;

        _players = new List<Player>();
        _playersIndexes = new Dictionary<ulong, int>();
    }
    void OnApplicationQuit(){
        _singleton=null;

        _localPlayer = null;
        _players = null;
        _playersIndexes = null;
    }
    public void SpawnPlayer(ulong clientID){
        Debug.Log("Spawning Player: "+clientID);
        var player = Instantiate(playerPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientID);
        
        player.cash.Value = Match.Singleton.startingCash;

        _playersIndexes[clientID] = _players.Count;//will increase by one next frame

        if(LobbyManager.Singleton.lobby != null){
            var playerIndex = GameSessionManager.Singleton.ClientIdToLobbyIndex(clientID);
            var nickname = LobbyManager.Singleton.lobby.Players[playerIndex].Data["Nickname"].Value;

            player.SetNickname(nickname);
        }
    }

    public void SpawnPlayers(){
        foreach(var clientID in NetworkManager.Singleton.ConnectedClientsIds){
            SpawnPlayer(clientID);
        }
    }
    public void SetLocalPlayer(Player player){
        _localPlayer = player;
    }
    public void AddPlayer(Player player){
        _players.Add(player);
    }
}
