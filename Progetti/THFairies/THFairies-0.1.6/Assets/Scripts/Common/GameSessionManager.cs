using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class GameSessionManager : NetworkBehaviour
{
    private static GameSessionManager _singleton;
    public static GameSessionManager Singleton { get { return _singleton;}} 
    private Dictionary<ulong, int> lobbyClientIds;
    void Awake(){
        _singleton = this;

        lobbyClientIds = new Dictionary<ulong,int>();

        DontDestroyOnLoad(gameObject);

        WaitNetworkManager(()=>{

            NetworkManager.Singleton.OnServerStarted += ()=>{
                if(!IsServer) return;
                if(Match.Singleton == null) return;
                Match.Singleton.StartMatch();
            };

            NetworkManager.Singleton.ConnectionApprovalCallback += (request, response) => {

                if(LobbyManager.Singleton.lobby == null){//It's the host cause relay boots first than lobby
                    response.Approved = true;

                    lobbyClientIds.Add(NetworkManager.LocalClientId, 0);

                    return;
                }

                var clientId = request.ClientNetworkId;

                var connectionData = request.Payload;
                var nickname = System.Text.Encoding.ASCII.GetString(connectionData);

                int index = 0;

                foreach(var player in LobbyManager.Singleton.lobby.Players){
                    if(player.Data["Nickname"].Value == nickname){
                        lobbyClientIds.Add(NetworkManager.LocalClientId, index);
                        response.Approved = true;
                        return;
                    }
                }

                response.Reason = "Not in the server lobby";

                response.Approved = false;
            };


            NetworkManager.Singleton.OnClientConnectedCallback += (clientId)=>{
                if(!IsServer) return;
                if(Match.Singleton == null) return;
                PlayerManager.Singleton.SpawnPlayer(clientId);
            };
        });
    }
    void Start(){
        WaitMatch(()=>{
            if(NetworkManager.Singleton.IsListening){
                PlayerManager.Singleton.SpawnPlayers();
                Match.Singleton.StartMatch();
            }
        });
    }
    void OnApplicationQuit(){
        _singleton=null;
    }
    async void WaitMatch(Action callback){
        while(Match.Singleton == null){
            await Task.Yield();
        }
        callback?.Invoke();
    }
    async void WaitNetworkManager(Action callback){
        while(NetworkManager.Singleton == null){
            await Task.Yield();
        }
        callback?.Invoke();
    }
    public int ClientIdToLobbyIndex(ulong clientId){
        return lobbyClientIds[clientId];
    }
}
