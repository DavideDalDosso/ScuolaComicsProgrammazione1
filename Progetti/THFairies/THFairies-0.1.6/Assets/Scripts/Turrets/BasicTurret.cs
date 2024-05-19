using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class BasicTurret : TurretBase
{
    [SerializeField] BasicTurretSO turretSO;
    [SerializeField] Transform turretTransform;
    [SerializeField] GameObject rangeCylinder;
    private AudioSource audioSource;
    private Animator animator;
    public NetworkVariable<float> range;
    private NetworkVariable<float> turnAngle;
    private float turnRate;
    private float fireTime;
    private float fireRate;
    private float damage;
    private float bulletSpeed;
    void Awake(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        turnRate = turretSO.turnRate * Mathf.Deg2Rad;
        range = new NetworkVariable<float>(turretSO.range);
        turnAngle = new NetworkVariable<float>();
        fireRate = turretSO.fireRate;
        damage = turretSO.damage;
        bulletSpeed = turretSO.bulletSpeed;

        range.OnValueChanged += (prevValue, newValue)=>{
            SetRangeDisplaySize(newValue);
        };
    }
    public override void OnNetworkSpawn()
    {
        SetRangeDisplaySize(range.Value);
    }
    void Update(){
        turretTransform.rotation = Quaternion.Euler(new Vector3(0, 0, turnAngle.Value * Mathf.Rad2Deg ));
    }
    void FixedUpdate() {
        if(!IsServer) return;

        var targets = TargettingSystem.First(transform.position, range.Value, 1);
        if(targets.Length > 0){
            var target = targets[0];

            var targetDirection = (target.transform.position - transform.position).normalized;
            var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x);

            var angleDisplacement = targetAngle - turnAngle.Value;
            var absAngleDisplacement = Mathf.Abs(angleDisplacement);

            var turnThisFrame = turnRate * Time.fixedDeltaTime;

            if(absAngleDisplacement > 180 * Mathf.Deg2Rad){
                angleDisplacement *= -1;
            }

            if(absAngleDisplacement < turnThisFrame){
                turnAngle.Value = targetAngle;

                if(Time.timeSinceLevelLoad > fireTime){
                    fireTime = Time.timeSinceLevelLoad + fireRate;

                    Shoot(target);
                }

            } else {

                if(angleDisplacement > 0){
                    turnAngle.Value += turnThisFrame;
                } else {
                    turnAngle.Value -= turnThisFrame;
                }
            }

            if(turnAngle.Value > 180 * Mathf.Deg2Rad){
                turnAngle.Value -= 360 * Mathf.Deg2Rad;
            } else if (turnAngle.Value < -180 * Mathf.Deg2Rad){
                turnAngle.Value += 360 * Mathf.Deg2Rad;
            }
        }
    }
    protected override void OnUpgrade(int upgradeIndex)
    {
        
    }
    void SetRangeDisplaySize(float size){
        rangeCylinder.transform.localScale = new Vector3(size, 0.01f, size);
    }

    void Shoot(EnemyBase target){
        audioSource.PlayOneShot(turretSO.shootSound, 1);

        animator.CrossFade("BasicTurretShootNormal", 0);
        var duration = animator.GetCurrentAnimatorStateInfo(0).length;
        animator.SetFloat("AnimSpeed", duration/fireRate);

        var bullet = Instantiate(turretSO.bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<NetworkObject>().Spawn();

        var destination = target.transform.position;

        bullet.onFixedUpdate = ()=>{

            if(target == null){
                var desinationDistance = (bullet.transform.position - destination).sqrMagnitude;
                if(desinationDistance < bulletSpeed * 0.05f){
                    bullet.GetComponent<NetworkObject>().Despawn();
                    return;
                }
            } else {
                destination = target.transform.position;
            }

            var direction = (-bullet.transform.position + destination).normalized * bulletSpeed;
            bullet.transform.position += direction * Time.fixedDeltaTime;

            var targetAngle = Mathf.Atan2(direction.y, direction.x);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle * Mathf.Rad2Deg ));
        };

        bullet.onHit = (enemy)=>{
            enemy.Damage(damage);
            if(bullet.IsSpawned) 
                bullet.GetComponent<NetworkObject>().Despawn();

            BulletParticlesClientRpc(bullet.transform.position, bullet.transform.eulerAngles.z);
        };
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void ShootClientRpc(){
        audioSource.PlayOneShot(turretSO.shootSound, 1);
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void BulletParticlesClientRpc(Vector3 position, float rotationAngle){
        var particleRotation = Quaternion.Euler(-rotationAngle, 90, -90);
        var particleObject = Instantiate(turretSO.bulletParticleObject, position, particleRotation).GetComponent<ParticleSystem>();

        particleObject.Play();

        CleanUpBullet(particleObject);
    }
    public async void CleanUpBullet(ParticleSystem particleSystem){
        while(particleSystem.isPlaying){
            await Task.Yield();
        }
        Destroy(particleSystem.gameObject);
    }
}
