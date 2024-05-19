using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DefeatSystem : GameObject
{
    private bool lost = false;
    public override void Render(Renderer renderer)
    {
        renderer.Rect('G', 0, 62, 239, 1);

        if(lost)
        {
            renderer.Text("YOU LOST!!!1!", 110, 30);
        }
    }

    public override void Start()
    {
        
    }

    public override void Update(float dt)
    {
        Asteroid[] asteroids = scene.GetGameObjects<Asteroid>("Asteroid");

        foreach(Asteroid asteroid in asteroids)
        {
            if(asteroid.y > 60)
            {
                lost = true;
            }
        }

        if(lost)
        {
            foreach(var asteroid in asteroids)
            {
                scene.Remove(asteroid);
            }

            foreach (var building in scene.GetGameObjects<Building>("Building"))
            {
                scene.Remove(building);
            }

            scene.GetSingleton<AsteroidSystem>("AsteroidSystem").running = false;
            scene.GetSingleton<BuildSystem>("BuildSystem").running = false;
            scene.GetSingleton<DifficultySystem>("DifficultySystem").running = false;
            scene.GetSingleton<MaterialSystem>("MaterialSystem").running = false;
        }
    }
}
