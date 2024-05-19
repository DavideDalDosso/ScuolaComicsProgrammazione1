using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TargettingSystem : NetworkBehaviour
{
    private static TargettingSystem singleton;
    private List<EnemyBase> enemies;
    void Awake()
    {
        singleton = this;
        enemies = new List<EnemyBase>();

        EnemyManager.Singleton.onEnemyDied += (enemy)=>{
            enemies.Remove(enemy);
        };
        EnemyManager.Singleton.onEnemyReachEnd += (enemy)=>{
            enemies.Remove(enemy);
        };
    }
    void OnApplicationQuit(){
        singleton = null;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemies = new List<EnemyBase>( EnemyManager.enemies );
        enemies.Sort(delegate(EnemyBase x, EnemyBase y){
            return x.distance.CompareTo(y.distance);
        });
    }

    public static EnemyBase[] First(Vector3 position, float range, int count){
        EnemyBase[] result = new EnemyBase[count];
        int resultIndex = 0;

        for(int i = 0; i < singleton.enemies.Count; i++){
            if(resultIndex == count) break;
            var enemy = singleton.enemies[i];
            var distance = (position-enemy.transform.position).magnitude;
            if(distance < range){
                result[resultIndex] = enemy;
                resultIndex++;
            }
        }

        if(resultIndex != count) {
            var smallerResult = new EnemyBase[resultIndex];
            for(int i=0; i<resultIndex;i++){
                smallerResult[i] = result[i];
            }
            result = smallerResult;
        }

        return result;
    }
}
