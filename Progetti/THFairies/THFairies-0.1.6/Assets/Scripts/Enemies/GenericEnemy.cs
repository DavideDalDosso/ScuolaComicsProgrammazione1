using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class GenericEnemy : EnemyBase
{
    [SerializeField] GenericEnemySO enemySO;
    private AudioSource audioSource;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    public override void OnNetworkSpawn(){
        
        maxHealth = enemySO.health;

        if(!IsServer) return;

        health.Value = maxHealth;
        speed = enemySO.speed;
        cashReward = enemySO.cashReward;
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        HitClientRpc();
    }

    public override void Die()
    {
        DeathClientRpc(transform.position);
        
        base.Die();
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void HitClientRpc(){
        audioSource.PlayOneShot(enemySO.hitSound, 1);
    }
        
    [Rpc(SendTo.ClientsAndHost)]
    public void DeathClientRpc(Vector3 position){
        var soundObject = Instantiate(enemySO.deathSoundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();

        soundObject.PlayOneShot(enemySO.deathSound, 1);

        var particleObject = Instantiate(enemySO.deathParticlesObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

        particleObject.Play();

        CleanUpDeath(soundObject, particleObject);
    }
    public async void CleanUpDeath(AudioSource audioSource, ParticleSystem particleSystem){
        while(particleSystem.isPlaying){
            await Task.Yield();
        }
        Destroy(particleSystem.gameObject);

        while(audioSource.isPlaying){
            await Task.Yield();
        }
        Destroy(audioSource.gameObject);
    }
}
