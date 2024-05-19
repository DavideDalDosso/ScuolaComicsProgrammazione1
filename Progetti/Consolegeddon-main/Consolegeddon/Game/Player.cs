using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

class Player : GameObject
{
    public float speed = 15;
    private BuildSystem buildSystem;

    public override void Start()
    {
        buildSystem = scene.GetSingleton<BuildSystem>("BuildSystem");
    }
    public override void Render(Renderer renderer)
    {
        int normX = (int)MathF.Round(x);
        int normY = (int)MathF.Round(y);

        renderer.Circle('#', normX, normY, 1f);
        renderer.Text('P', normX, normY);

        renderer.Text("Playerr", normX - 3, normY - 2);
        //renderer.Line('P', normX, normY, normX-20, normY-10);
        //renderer.Rect('#', normX - 3, normY - 5, 7, 3); //DEBUG REASONS
    }

    public override void Update(float dt)
    {
        EngineInput input = scene.GetEngine().GetInput();

        if(input.KeyAvailable(ConsoleKey.D))
        {
            x += 1 * dt * speed;
        }
        if (input.KeyAvailable(ConsoleKey.A))
        {
            x -= 1 * dt * speed;
        }
        if (input.KeyAvailable(ConsoleKey.W))
        {
            y -= 1 * 0.6f * dt * speed;
        }
        if (input.KeyAvailable(ConsoleKey.S))
        {
            y += 1 * 0.6f * dt * speed;
        }

        WallCollision();

        if(input.KeyAvailable(ConsoleKey.R))
        {
            buildSystem.SpawnMiner(x, y);
        }
        if (input.KeyAvailable(ConsoleKey.Q))
        {
            buildSystem.SpawnTurret(x, y);
        }
        if (input.KeyAvailable(ConsoleKey.E))
        {
            buildSystem.SpawnForcefield(x, y);
        }
    }

    private void WallCollision()
    {
        if(x < 0)
        {
            x = 0;
        }
        if (y < 0)
        {
            y = 0;
        }
        if(x >= Console.BufferWidth-1)
        {
            x = Console.BufferWidth-1;
        }
        if(y >= Console.BufferHeight-1)
        {
            y = Console.BufferHeight-1;
        }
    }
}
