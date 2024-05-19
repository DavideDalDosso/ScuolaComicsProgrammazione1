using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public delegate void OnHit(EnemyBase enemy);
    public OnHit onHit;
    public delegate void OnFixedUpdate();
    public OnFixedUpdate onFixedUpdate;
    private TurretBase owner;
    void FixedUpdate()
    {
        if(onFixedUpdate != null) onFixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var enemy = other.gameObject.GetComponent<EnemyBase>();
        if(onHit != null) onHit(enemy);
    }
    public void SetOwner(TurretBase owner){
        this.owner = owner;
    }
}
