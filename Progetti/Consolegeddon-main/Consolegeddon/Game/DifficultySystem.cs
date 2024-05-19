using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DifficultySystem : GameObject
{
    public bool running = true;
    public float nextDifficultyCooldown { get; set; }
    private float nextDifficulty { get; set; }
    public int nextTierCooldown { get; set; }
    private int nextTier { get; set; }
    private int difficulty = 0;
    private AsteroidSystem? asteroidSystem { get; set; }

    public override void Render(Renderer renderer)
    {
        renderer.Text("Difficulty: " + difficulty + " (" + nextDifficulty + ") \\ (" + nextTier + ")", 0, 5);
        renderer.Text("Speed: " + asteroidSystem.spawnSpeed, 0, 6);
        renderer.Text("Health: " + asteroidSystem.spawnHealth, 0, 7);
        renderer.Text("Size: " + asteroidSystem.spawnSize, 0, 8);
        renderer.Text("Cooldown: " + asteroidSystem.spawnCooldown, 0, 9);
    }
    public override void Init(Scene scene)
    {
        base.Init(scene);
        scene.AddTag("DifficultySystem", this);
    }
    public override void Start()
    {
        asteroidSystem = scene.GetSingleton<AsteroidSystem>("AsteroidSystem");
        nextDifficulty = nextDifficultyCooldown;
        nextTier = nextTierCooldown;
    }

    public override void Update(float dt)
    {
        if (!running) return;

        nextDifficulty -= dt;
        if(nextDifficulty <= 0)
        {
            difficulty++;
            nextDifficulty += nextDifficultyCooldown * (1 + 0.05f * difficulty);

            asteroidSystem.spawnCooldown *= 0.98f;
            asteroidSystem.spawnSpeed *= 1.02f;
            asteroidSystem.spawnHealth++;

            nextTier--;

            if(nextTier <= 0)
            {
                nextTier = nextTierCooldown;

                asteroidSystem.spawnSize *= 3;
                asteroidSystem.spawnCooldown *= 4f;
                asteroidSystem.spawnHealth *= 2;
                asteroidSystem.spawnSpeed *= 0.7f;
            }
        }
    }
}
