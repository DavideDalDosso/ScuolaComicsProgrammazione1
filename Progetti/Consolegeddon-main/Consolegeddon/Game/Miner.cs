using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Miner : Building
{
    private MaterialSystem? materialSystem;

    public override void Init(Scene scene)
    {
        base.Init(scene);
        scene.AddTag("Miner", this);
        scene.AddTag("Building", this);
    }
    public override void Start()
    {
        materialSystem = scene.GetSingleton<MaterialSystem>("MaterialSystem");
    }
    public override void Render(Renderer renderer)
    {
        int normX = (int)MathF.Round(x);
        int normY = (int)MathF.Round(y);

        renderer.Circle('M', normX, normY, size);
        renderer.Text('O', normX, normY);
    }

    public override void Update(float dt)
    {
        materialSystem.materials += dt * 2f;
    }
}
