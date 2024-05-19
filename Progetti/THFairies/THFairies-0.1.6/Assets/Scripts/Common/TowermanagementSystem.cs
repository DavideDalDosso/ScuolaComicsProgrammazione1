using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TowerManagementSystem : NetworkBehaviour
{
    private static TowerManagementSystem _singleton;
    public static TowerManagementSystem Singleton {get {return _singleton;}}
    
    void Awake()
    {
        _singleton = this;
    }
    
    void OnApplicationQuit() {
        _singleton = null;
    }
}
