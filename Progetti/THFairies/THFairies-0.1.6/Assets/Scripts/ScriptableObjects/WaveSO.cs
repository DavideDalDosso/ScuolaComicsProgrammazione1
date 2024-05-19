using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "GOFairies/Wave", order = 20)]
public class WaveSO : ScriptableObject
{
    public WaveBuffer[] waveBuffers;
    public float waveTime;
    public int cashReward;
    [Serializable]
    public struct WaveBuffer {
        public int spawnerIndex;
        public EnemySO enemySO;
        public int amount;
        public float spawnRate;
        public float spawnDelay;
    }
}
