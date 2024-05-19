using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

class Player : IGameObject
{
    public float x;
    public float y;

    public void Render(Renderer renderer)
    {
        renderer.Circle('#', (int)MathF.Round(x), (int)MathF.Round(y), 1f, 1f);
        renderer.Rect('O', (int)MathF.Round(x), (int)MathF.Round(y), 1, 1);
    }

    public void Update(float dt, Scene scene)
    {
        EngineInput input = scene.GetEngine().GetInput();

        if(input.KeyAvailable(ConsoleKey.D))
        {
            x += 1;
        }
        if (input.KeyAvailable(ConsoleKey.A))
        {
            x -= 1;
        }
        if (input.KeyAvailable(ConsoleKey.W))
        {
            y -= 1 * 0.6f;
        }
        if (input.KeyAvailable(ConsoleKey.S))
        {
            y += 1 * 0.6f;
        }
    }
}
