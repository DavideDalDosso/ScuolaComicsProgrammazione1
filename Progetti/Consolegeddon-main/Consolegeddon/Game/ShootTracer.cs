using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootTracer : GameObject
{
    public float life { get; set; }
    public float x1 { get; set; }
    public float y1 { get; set; }
    public float x2 { get; set; }
    public float y2 { get; set; }
    private Scene scene;
    public override void Init(Scene scene)
    {
        this.scene = scene;
    }

    public override void Render(Renderer renderer)
    {
        int normX1 = (int) MathF.Round(x1);
        int normY1 = (int) MathF.Round(y1);
        int normX2 = (int) MathF.Round(x2);
        int normY2 = (int) MathF.Round(y2);
        renderer.Line('S', normX1, normY1, normX2, normY2);
    }

    public override void Start()
    {
        
    }

    public override void Update(float dt)
    {
        life -= dt;
        if(life <= 0)
        {
            scene.Remove(this);
        }
    }
}
