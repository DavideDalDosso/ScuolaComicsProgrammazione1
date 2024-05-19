using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemySO : ScriptableObject
{
    public EnemyBase enemyPrefab;
    public float health;
    public float speed;
    public int cashReward;
}
