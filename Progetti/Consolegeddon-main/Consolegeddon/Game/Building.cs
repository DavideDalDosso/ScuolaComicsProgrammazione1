using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Building : GameObject, IDamageable
{
    public int health { get; set; }
    public int size { get; set; }
    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            scene.Remove(this);
        }
    }
}
