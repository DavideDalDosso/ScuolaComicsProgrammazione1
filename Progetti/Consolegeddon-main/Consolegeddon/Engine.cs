using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

class Engine
{
    private Scene? currentScene;
    private Renderer renderer = new Renderer();
    private EngineInput input = new EngineInput();
    public int maxFPS {  get; set; }
    public int maxUpdates { get; set; }
    public void Start()
    {
        DateTime time1 = DateTime.Now;
        DateTime time2;

        float updateTimer = 0;
        float renderTimer = 0;
        float secondTimer = 0;

        int updates = 0;
        int renders = 0;

        int bufferedUpdates = 0;
        int bufferedRenders = 0;

        // Here we find DeltaTime in while loop
        while (true)
        {
            time2 = DateTime.Now;

            //We convert the date instances into numbers we can subtract with eachother
            float deltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
            updateTimer += deltaTime;
            renderTimer += deltaTime;
            if(updateTimer > 1f / maxUpdates)
            {

                if (Console.KeyAvailable)
                {
                    input.Register(Console.ReadKey(true));
                }

                secondTimer += updateTimer;
                if (updateTimer > 0.5f) updateTimer = 0.5f;
                updates++;

                input.UpdateAvailable();

                currentScene?.Update(updateTimer);

                updateTimer -= 1f / maxUpdates;

                if (secondTimer > 1)
                {
                    secondTimer -= 1;
                    bufferedUpdates = updates;
                    bufferedRenders = renders;
                    updates = 0;
                    renders = 0;
                }
            }
            if( renderTimer > 1f / maxFPS)
            {
                renders++;

                renderer.Rect(' ', 0, 0, Console.WindowWidth, Console.WindowHeight);
                currentScene?.Render(renderer);
                renderer.Text(bufferedRenders + " FPS " + bufferedUpdates + "UPD", 0, 0);

                renderTimer -= 1f / maxFPS;
            }

            time1 = time2;
        }
    }
    public void SetScene(Scene scene)
    {
        currentScene = scene;
    }
    public EngineInput GetInput()
    {
        return input;
    }
}
