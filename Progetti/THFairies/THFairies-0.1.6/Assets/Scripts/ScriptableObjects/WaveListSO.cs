using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveList", menuName = "GOFairies/WaveList", order = 10)]
public class WaveListSO : ScriptableObject
{
    public WaveSO[] waves;
}
