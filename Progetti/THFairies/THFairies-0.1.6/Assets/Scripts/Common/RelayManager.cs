using System.Collections;
using System.Collections.Generic;
using Unity.Services.Relay;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using System;
using WebSocketSharp;

public class RelayManager : MonoBehaviour
{
    private static RelayManager _singleton;
    public static RelayManager Singleton {get {return _singleton;}}
    private string _joinCode;
    public string joinCode {get {return _joinCode;}}
    
    void Awake()
    {
        _singleton = this;

        DontDestroyOnLoad(gameObject);
    }  
    void OnApplicationQuit() {
        _singleton = null;
    }
    public void CreateRelay(Action<string> callback){
        CreateRelayAsync(callback);
    }
    private async void CreateRelayAsync(Action<string> callback){
        try{

            var allocation = await RelayService.Instance.CreateAllocationAsync(6);

            _joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort) allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(LocalPlayerData.nickname.ToString().ToCharArray());

            NetworkManager.Singleton.StartHost();

            callback?.Invoke(joinCode);

        } catch(RelayServiceException e){
            Debug.Log(e);
            callback?.Invoke("");
        }
    }
    public void JoinRelay(string joinCode, Action callback){
        JoinRelayAsync(joinCode, callback);
    }
    private async void JoinRelayAsync(string joinCode, Action callback){
        try{
            var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                allocation.RelayServer.IpV4,
                (ushort) allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                allocation.HostConnectionData
            );
            NetworkManager.Singleton.StartClient();

            callback.Invoke();

        } catch(RelayServiceException e){
            Debug.Log(e);
        }
    }
}
