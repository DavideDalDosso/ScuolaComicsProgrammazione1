using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BuildSystem : GameObject
{
    public bool running = true;
    public float minerPrice { get; set; }
    public float turretPrice { get; set; }
    public float forcefieldPrice { get; set; }
    private float increasedMinerPrice = 0;
    private float increasedTurretPrice = 0;
    private MaterialSystem? materialSystem;
    private Scene? scene;

    public override void Init(Scene scene)
    {
        this.scene = scene;
        scene.AddTag("BuildSystem", this);
    }
    public override void Start()
    {
        materialSystem = scene.GetSingleton<MaterialSystem>("MaterialSystem");
    }
    public override void Render(Renderer renderer)
    {
        float clampMiner = MathF.Round(increasedMinerPrice, 1);
        renderer.Text("[R] Miner: " + clampMiner, 180, 2);
        renderer.Text("Increases materials per second by 2 [3 hp]", 180, 3);
        float clampTurret = MathF.Round(increasedTurretPrice, 1);
        renderer.Text("[Q] Turret: " + clampTurret, 180, 4);
        renderer.Text("Deals 1 damage every second [3 hp]", 180, 5);
        float clampWall = MathF.Round(forcefieldPrice, 1);
        renderer.Text("[E] Forcefield: " + clampWall, 180, 6);
        renderer.Text("Is able to block asteroids from x3 it's radius [25 hp]", 180, 7);
    }

    public override void Update(float dt)
    {
        if (!running) return;
        int miners = scene.GetGameObjects<Miner>("Miner").Length;
        increasedMinerPrice = minerPrice + minerPrice * miners * miners * 0.5f;
        int turrets = scene.GetGameObjects<Turret>("Turret").Length;
        increasedTurretPrice = turretPrice + turretPrice * turrets * turrets * 0.3f;
    }

    public void SpawnMiner(float x, float y)
    {
        if (!running) return;
        if (materialSystem.materials >= increasedMinerPrice)
        {
            materialSystem.materials -= increasedMinerPrice;

            Miner miner = new Miner();
            miner.x = x;
            miner.y = y;
            miner.health = 3;
            miner.size = 2;

            scene.Add(miner);
        }
    }

    public void SpawnTurret(float x, float y)
    {
        if (!running) return;
        if (materialSystem.materials >= increasedTurretPrice)
        {
            materialSystem.materials -= increasedTurretPrice;

            Turret turret = new Turret();
            turret.x = x;
            turret.y = y;
            turret.health = 3;
            turret.shootCooldown = 0.5f;
            turret.size = 3;

            scene.Add(turret);
        }
    }

    public void SpawnForcefield(float x, float y)
    {
        if (!running) return;
        if (materialSystem.materials >= forcefieldPrice)
        {
            materialSystem.materials -= forcefieldPrice;

            Forcefield wall = new Forcefield();
            wall.x = x;
            wall.y = y;
            wall.health = 25;
            wall.size = 9;

            scene.Add(wall);
        }
    }
}
