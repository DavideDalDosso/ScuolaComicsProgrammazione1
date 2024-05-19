using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class DebugConnectionUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    void Start()
    {

        WaitNetworkManager(()=>{
            if(NetworkManager.Singleton.IsListening)
            gameObject.SetActive(false);

            hostButton.onClick.AddListener(()=>{
                NetworkManager.Singleton.StartHost();
                gameObject.SetActive(false);
            });

            joinButton.onClick.AddListener(()=>{
                NetworkManager.Singleton.StartClient();
                gameObject.SetActive(false);
            });
        });

    }

    async void WaitNetworkManager(Action callback){
        while(NetworkManager.Singleton == null){
            await Task.Yield();
        }
        callback?.Invoke();
    }
}
