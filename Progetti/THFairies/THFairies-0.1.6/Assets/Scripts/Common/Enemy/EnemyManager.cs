using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyManager : NetworkBehaviour
{
    private static EnemyManager _singleton;
    public static EnemyManager Singleton { get { return _singleton;}} 
    private static List<EnemyBase> _enemies;
    public static List<EnemyBase> enemies {get { return _enemies; }}
    private static EnemySO[] _scriptableObjects;
    public static EnemySO[] ScriptableObjects {get{ return _scriptableObjects; }}
    public delegate void OnEnemyDied(EnemyBase enemy);
    public OnEnemyDied onEnemyDied;
    public delegate void OnEnemyReachEnd(EnemyBase enemy);
    public OnEnemyReachEnd onEnemyReachEnd;
    void Awake(){
        _singleton = this;

        _enemies = new List<EnemyBase>();
        _scriptableObjects = Resources.FindObjectsOfTypeAll(typeof(EnemySO)) as EnemySO[];
    }
    void OnApplicationQuit(){
        _singleton = null;

        _enemies = null;
        _scriptableObjects = null;
    }
    public EnemyBase SpawnEnemy(EnemySO enemySO, Vector3 position){
        var enemy = Instantiate(enemySO.enemyPrefab, position, Quaternion.identity).GetComponent<EnemyBase>();
        enemy.GetComponent<NetworkObject>().Spawn();

        return enemy;
    }

    public void AddEnemy(EnemyBase enemy){
        enemy.onDied +=()=>{
            _enemies.Remove(enemy);
        };
        enemy.onReachEnd +=()=>{
            _enemies.Remove(enemy);
        };

        _enemies.Add(enemy);
    }
}
