using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private float offsetRadius;
    private List<EnemySpawnerBuffer> waveBuffers = new List<EnemySpawnerBuffer>();
    private void FixedUpdate() {

        if(waveBuffers.Count > 0){

            List<EnemySpawnerBuffer> toRemove = new List<EnemySpawnerBuffer>();

            foreach(var buffer in waveBuffers){
                if(buffer.amount == 0){
                    toRemove.Add(buffer);
                    continue;
                }

                if(Time.timeSinceLevelLoad > buffer.timeForSpawn){
                    buffer.amount--;
                    buffer.timeForSpawn += buffer.spawnRate;

                    var enemy = EnemyManager.Singleton.SpawnEnemy(buffer.enemySO, transform.position);
                    var enemyMovement = enemy.GetComponent<EnemyMovement>();
                    
                    enemyMovement.SetPath(enemyPath);
                    enemyMovement.SetOffset( new Vector3( Random.Range(-offsetRadius, offsetRadius), Random.Range(-offsetRadius, offsetRadius) ) );
                }
            }

            foreach(var buffer in toRemove){
                waveBuffers.Remove(buffer);
            }
        }
    }
    public void AddWaveBuffer(WaveSO.WaveBuffer waveBuffer){

        var enemySpawnerBuffer = new EnemySpawnerBuffer();
        enemySpawnerBuffer.enemySO = waveBuffer.enemySO;
        enemySpawnerBuffer.amount = waveBuffer.amount;
        enemySpawnerBuffer.timeForSpawn = Time.timeSinceLevelLoad + waveBuffer.spawnDelay;
        enemySpawnerBuffer.spawnRate = waveBuffer.spawnRate;

        waveBuffers.Add(enemySpawnerBuffer);
    }

    private class EnemySpawnerBuffer
    {
        public EnemySO enemySO;
        public int amount;
        public float timeForSpawn;
        public float spawnRate;
    }
}
