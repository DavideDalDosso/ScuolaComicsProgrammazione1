using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;

public class AuthenticationManager : MonoBehaviour
{
    private static AuthenticationManager _singleton;
    public static AuthenticationManager Singleton {get {return _singleton;}}
    void Awake()
    {
        _singleton = this;

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        AuthenticateAnonimously(null);
    }
    public void AuthenticateAnonimously(Action callback){
        AuthenticateAnonimouslyAsync(callback);
    }
    private async void AuthenticateAnonimouslyAsync(Action callback){
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += ()=>{
            Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        callback?.Invoke();
    }
}
