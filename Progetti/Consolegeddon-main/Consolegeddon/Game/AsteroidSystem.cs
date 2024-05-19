using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

class AsteroidSystem : GameObject
{
    public bool running = true;
    private float timer = 0;
    public float spawnSize { get; set; }
    public float spawnCooldown { get; set; }
    public float spawnSpeed { get; set; }
    public int spawnHealth { get; set; }
    public override void Init(Scene scene)
    {
        this.scene = scene;
        scene.AddTag("AsteroidSystem", this);
    }

    public override void Render(Renderer renderer)
    {
        renderer.Text("Asteroid CD: " + (spawnCooldown - timer), 0, 2);
    }

    public override void Start()
    {
        
    }

    public override void Update(float dt)
    {
        scene.AddTag("AsteroidSystem", this);
        if (!running) return;
        timer += dt;
        if(timer >= spawnCooldown)
        {
            timer -= spawnCooldown;

            var random = scene.GetRandom();

            Asteroid asteroid = new Asteroid();
            asteroid.x = random.Next(220);
            asteroid.y = -spawnSize;
            asteroid.size = spawnSize;
            asteroid.speed = spawnSpeed;
            asteroid.velX = random.NextSingle() * 2 - 1;
            asteroid.velY = random.NextSingle() * 2 - 1;
            asteroid.maxHealth = spawnHealth;
            scene.Add(asteroid);
        }
    }
}
