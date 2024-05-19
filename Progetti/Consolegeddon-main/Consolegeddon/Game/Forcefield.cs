using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Forcefield : Building
{

    public override void Init(Scene scene)
    {
        base.Init(scene);
        scene.AddTag("Building", this);
    }
    public override void Render(Renderer renderer)
    {
        int normX = (int)MathF.Round(x);
        int normY = (int)MathF.Round(y);

        renderer.Circle('F', normX, normY, size/3);
        renderer.Text(health.ToString(), normX, normY);
    }

    public override void Start()
    {

    }

    public override void Update(float dt)
    {
        
    }
}
