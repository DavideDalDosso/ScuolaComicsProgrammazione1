using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class GameUIBoorstrapper : MonoBehaviour
{
    [SerializeField] GameUI gameUIPrefab;
    [SerializeField] Canvas canvas;
    void Start()
    {
        WaitLocalPlayer(()=>{
            var gameUI = Instantiate(gameUIPrefab, canvas.transform);
            gameUI.Setup(Match.Singleton, PlayerManager.localPlayer);
        });
    }

    async void WaitLocalPlayer(Action callback){
        while(PlayerManager.localPlayer == null){
            await Task.Yield();
        }
        callback?.Invoke();
    }
}
