using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Asteroid : GameObject, IDamageable
{
    public float speed { get; set; }
    public float size { get; set; }
    public float velX { get; set; }
    public float velY { get; set; }
    public int health { get; set; }
    public int maxHealth { get; set; }
    private Scene? scene;
    public override void Init(Scene scene)
    {
        this.scene = scene;
        scene.AddTag("Asteroid", this);
    }

    public override void Render(Renderer renderer)
    {
        int normX = (int)MathF.Round(x);
        int normY = (int)MathF.Round(y);

        renderer.Circle('A', normX, normY, size);
        renderer.Text(health.ToString() , normX, normY);
    }

    public override void Start()
    {
        health = maxHealth;
    }

    public override void Update(float dt)
    {
        if(health > 0) scene.AddTag("Asteroid", this);//WORKAROUND FOR LAG
        x += velX * speed;
        y += velY * speed;

        if (velY < 1) velY += dt * 0.5f;

        if (x < 0)
        {
            x = 0; velX = -velX;
        }
        if (x > 240)
        {
            x = 240; velX = -velX;
        }

        Building building = scene.GetClosest<Building>("Building", x, y);
        if(building != null)
        {
            float offsetX = building.x - x;
            float offsetY = building.y - y;
            float distancePow = offsetX * offsetX + offsetY * offsetY;

            if (MathF.Sqrt(distancePow) < size + building.size + 2)
            {
                y += 1;
                Damage(10);
                building.Damage(1);
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            scene.Remove(this);

            if (size < 1.4f) return;

            var random = scene.GetRandom();

            for(int i=0; i < 3; i++)
            {
                Asteroid asteroid = new Asteroid();
                asteroid.x = x;
                asteroid.y = y;
                asteroid.size = MathF.Ceiling( size / 2 );
                asteroid.speed = speed * 1.2f;
                asteroid.velX = random.NextSingle() * 2 - 1;
                asteroid.velY = random.NextSingle() * 2 - 1;
                asteroid.maxHealth = maxHealth/3;
                scene.Add(asteroid);
            }
        }
    }
}
