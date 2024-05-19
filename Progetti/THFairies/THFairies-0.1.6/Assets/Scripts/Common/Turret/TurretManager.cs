using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurretManager : NetworkBehaviour
{
    private static TurretManager _singleton;
    public static TurretManager Singleton { get { return _singleton;}} 
    private static List<TurretBase> _turrets;
    public static List<TurretBase> turrets {get {return _turrets;}}
    private static TurretSO[] _scriptableObjects;
    public static TurretSO[] scriptableObjects {get{return _scriptableObjects;}}
    void Awake(){
        _singleton = this;

        _turrets = new List<TurretBase>();
        _scriptableObjects = Resources.FindObjectsOfTypeAll(typeof(TurretSO)) as TurretSO[];
    }
    void OnApplicationQuit(){
        _singleton = null;

        _turrets = null;
        _scriptableObjects = null;
    }
    public TurretBase SpawnTurret(TurretSO turretSO, Vector3 position){
        var turret = Instantiate(turretSO.turretPrefab, position, Quaternion.identity).GetComponent<TurretBase>();
        turret.GetComponent<NetworkObject>().Spawn();

        return turret;
    }

    public void AddTurret(TurretBase turret){
        turrets.Add(turret);
    }
}
