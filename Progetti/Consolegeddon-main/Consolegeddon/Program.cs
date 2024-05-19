

public class Program{

    public static void Main(string[] args)
    {
        Console.SetBufferSize(240, 63);
        Console.SetWindowSize(Console.BufferWidth, Console.BufferHeight);

        Engine engine = new Engine();
        engine.maxFPS = 5;
        engine.maxUpdates = 60;

        Scene scene = new Scene(engine);

        MaterialSystem materialSystem = new MaterialSystem();
        materialSystem.materials = 100;
        scene.Add(materialSystem);

        BuildSystem buildSystem = new BuildSystem();
        buildSystem.minerPrice = 20;
        buildSystem.turretPrice = 100;
        buildSystem.forcefieldPrice = 150;
        scene.Add(buildSystem);

        AsteroidSystem asteroidSystem = new AsteroidSystem();
        asteroidSystem.spawnCooldown = 10;
        asteroidSystem.spawnSize = 2;
        asteroidSystem.spawnSpeed = 0.1f;
        asteroidSystem.spawnHealth = 3;
        scene.Add(asteroidSystem);

        DifficultySystem difficultySystem = new DifficultySystem();
        difficultySystem.nextDifficultyCooldown = 15;
        difficultySystem.nextTierCooldown = 10;
        scene.Add(difficultySystem);

        DefeatSystem defeatSystem = new DefeatSystem();
        scene.Add(defeatSystem);

        Player player = new Player();
        player.x = 120;
        player.y = 50;
        player.speed = 60;
        scene.Add(player);

        engine.SetScene(scene);
        engine.Start();
    }

}