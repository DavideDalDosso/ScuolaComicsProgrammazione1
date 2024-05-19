using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class LobbyManager : MonoBehaviour
{
    private static LobbyManager _singleton;
    public static LobbyManager Singleton {get {return _singleton;}}
    private Lobby _lobby;
    public Lobby lobby {get {return _lobby;}}
    private float heartBeatTime;
    
    void Awake()
    {
        _singleton = this;

        DontDestroyOnLoad(gameObject);
    }
    void OnApplicationQuit() {
        _singleton = null;
    }
    void Update(){
        if(_lobby == null) return;
        if(Time.timeSinceLevelLoad > heartBeatTime){
            heartBeatTime += 15;
            LobbyService.Instance.SendHeartbeatPingAsync(_lobby.Id);
        }
    }
    public void CreateLobby(string joinCode, Action<string> callback){
        CreateLobbyAsync(joinCode, callback);
    }
    private async void CreateLobbyAsync(string joinCode, Action<string> callback){
        try{
            _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", 6, new CreateLobbyOptions(){
                Data = new Dictionary<string, DataObject> {
                    { "Level", new DataObject(DataObject.VisibilityOptions.Public, "Beginnings")},
                    { "JoinCode", new DataObject(DataObject.VisibilityOptions.Public, joinCode)}
                },
                Player = GetLocalPlayer()
            });

            callback?.Invoke(_lobby.LobbyCode);
        } catch(LobbyServiceException e){
            Debug.LogException(e);
            callback?.Invoke("");
        }
    }
    public void JoinLobby(string lobbyCode, Action<bool> callback){
        JoinLobbyAsync(lobbyCode, callback);
    }
    private async void JoinLobbyAsync(string lobbyCode, Action<bool> callback){
        try{
            _lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions{
                Player = GetLocalPlayer()
            });
            callback?.Invoke(true);
        } catch(LobbyServiceException e){
            Debug.LogException(e);
            callback?.Invoke(false);
        }
    }
    public void ListLobbies(Action<QueryResponse> callback){
        ListLobbiesAsync(callback);
    }
    private async void ListLobbiesAsync(Action<QueryResponse> callback){
        try{
            var query = await LobbyService.Instance.QueryLobbiesAsync();
            callback?.Invoke(query);
        } catch(LobbyServiceException e){
            Debug.LogException(e);
            callback?.Invoke(null);
        }
    }
    public void LeaveLobby(Action callback){
        try{
            LeaveLobbyAsync(callback);
        } catch (LobbyServiceException e){
            Debug.LogException(e);
        }
    }
    private async void LeaveLobbyAsync(Action callback){
        try{
            await LobbyService.Instance.RemovePlayerAsync(_lobby.Id, AuthenticationService.Instance.PlayerId);
            _lobby = null;
        } catch (LobbyServiceException e){
            Debug.LogException(e);
        }

        callback?.Invoke();
    }
    public Unity.Services.Lobbies.Models.Player GetLocalPlayer(){
        var player = new Unity.Services.Lobbies.Models.Player(){
            Data = new Dictionary<string, PlayerDataObject>{
                {"Nickname", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, LocalPlayerData.nickname.ToString())}
            }
        };
        return player;
    }
    public void UpdateLobby(Action callback){
        UpdateLobbyAsync(callback);
    }
    private async void UpdateLobbyAsync(Action callback){
        _lobby = await LobbyService.Instance.GetLobbyAsync(_lobby.Id);

        callback?.Invoke();
    }
}
