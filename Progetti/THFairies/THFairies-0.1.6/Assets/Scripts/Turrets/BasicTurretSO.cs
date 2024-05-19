using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTurret", menuName = "GOFairies/Turrets/BasicTurret", order = 0)]
public class BasicTurretSO : TurretSO
{
    public Bullet bullet;
    public float turnRate;
    public float damage;
    public float fireRate;
    public float bulletSpeed;
    public AudioClip shootSound;
    public ParticleSystem bulletParticleObject;
    public float upgrade3ExtraRange;
    public float upgrade3ExtraTurnRate;
}
