using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericEnemy", menuName = "GOFairies/Enemies/GenericEnemy", order = 0)]
public class GenericEnemySO : EnemySO
{
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioSource deathSoundObject;
    public ParticleSystem deathParticlesObject;
}
