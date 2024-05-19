using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class EnemyBase : NetworkBehaviour
{
    [SerializeField] protected float maxHealth;
    public NetworkVariable<float> health;
    public float speed {get; set;}
    public float distance {get; set;}
    public int cashReward {get; set;}
    public delegate void OnDied();
    public OnDied onDied;
    public delegate void OnReachEnd();
    public OnReachEnd onReachEnd;
    void Awake(){

        health = new NetworkVariable<float>();

    }
    void Start(){
        EnemyManager.Singleton.AddEnemy(this);
    }
    void FixedUpdate() {
        if(!IsServer) return;
        if(health.Value <= 0f){
            Die();
            if(onDied != null) onDied();
        }
    }
    public virtual void Damage(float damage){
        health.Value -= damage;
    }
    public virtual void Die()
    {
        foreach(var player in PlayerManager.players){
            player.cash.Value += cashReward;
        }
        GetComponent<NetworkObject>().Despawn();
    }
    public virtual void ReachEnd(){
        Match.Singleton.lives.Value--;
        if(onReachEnd!=null) onReachEnd();

        GetComponent<NetworkObject>().Despawn();
    }
}
