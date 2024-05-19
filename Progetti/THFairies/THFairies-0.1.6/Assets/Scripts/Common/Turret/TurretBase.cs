using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class TurretBase : NetworkBehaviour
{
    public TurretDataNetworkVariable upgrades = new TurretDataNetworkVariable();
    [Rpc(SendTo.Server)]
    public void UpgradeServerRpc(int upgradeIndex, RpcParams rpcParams){
        var clientID = rpcParams.Receive.SenderClientId;
        Debug.Log("Upgrade from clientID:"+clientID);
        OnUpgrade(upgradeIndex);
    }
    void Start(){
        TurretManager.Singleton.AddTurret(this);
    }
    protected abstract void OnUpgrade(int upgradeIndex);
}