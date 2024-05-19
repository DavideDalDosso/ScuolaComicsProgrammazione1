using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using System;

public class Match : NetworkBehaviour
{
    private static Match _singleton;
    public static Match Singleton { get { return _singleton;}} 
    [SerializeField] EnemySpawner[] enemySpawners;
    [SerializeField] WaveListSO waveList;
    [SerializeField] private int _startingCash;
    public int startingCash { get { return _startingCash;}}
    [SerializeField] private int _startingLives;
    [SerializeField] private AudioClip nextWaveSound;
    private AudioSource audioSource;
    public NetworkVariable<int> lives;
    public NetworkVariable<int> currentWave;
    public NetworkVariable<float> nextWaveTime;
    private bool running;
    public NetworkVariable<bool> isGameOver;
    void Awake()
    {
        _singleton = this;

        audioSource = GetComponent<AudioSource>();

        lives = new NetworkVariable<int>(_startingLives);
        currentWave = new NetworkVariable<int>(0);

        isGameOver = new NetworkVariable<bool>(true);
    }
    void OnApplicationQuit(){
        _singleton=null;
    }

    void FixedUpdate()
    {
        if(!IsServer) return;
        if(!running) return;

        if(lives.Value <= 0){
            EndMatch();
            return;
        }

        if( Time.timeSinceLevelLoad > nextWaveTime.Value ){
            if( currentWave.Value >= waveList.waves.Count() ){
                Debug.Log("Finished waves!");
                running = false;
                isGameOver.Value = false;
                EndMatch();
                return;
            }
            Debug.Log("Starting next wave!");
            StartWave();
        }
    }

    public void StartMatch(){
        running = true;

        nextWaveTime.Value = Time.timeSinceLevelLoad;
    }


    public void StartWave(){
        var cashReward = waveList.waves[currentWave.Value].cashReward;
        foreach(var player in PlayerManager.players){
            player.cash.Value += cashReward;
        }

        if(currentWave.Value!=0)
            NextWaveClientRpc();

        currentWave.Value++;

        var wave = waveList.waves[currentWave.Value-1];

        foreach(WaveSO.WaveBuffer buffer in wave.waveBuffers){
            var spawningIndex = buffer.spawnerIndex;
            if( spawningIndex < 0 || spawningIndex >= enemySpawners.Length){
                spawningIndex = 0;
            }

            enemySpawners[spawningIndex].AddWaveBuffer(buffer);
        }

        nextWaveTime.Value = Time.timeSinceLevelLoad + wave.waveTime;
    }
    private void EndMatch(){
        running = false;
        nextWaveTime.Value = -1f;
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void NextWaveClientRpc(){
        audioSource.PlayOneShot(nextWaveSound, 1);
    }
}
