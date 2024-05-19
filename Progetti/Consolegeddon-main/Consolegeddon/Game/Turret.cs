using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Turret : Building
{
    public float shootCooldown { get; set; }
    private float shootTimer = 0;
    private bool located;
    public override void Init(Scene scene)
    {
        base.Init(scene);
        scene.AddTag("Turret", this);
        scene.AddTag("Building", this);
    }
    public override void Start()
    {
        
    }
    public override void Render(Renderer renderer)
    {
        int normX = (int)MathF.Round(x);
        int normY = (int)MathF.Round(y);

        renderer.Circle('T', normX, normY, size);
        if (located)
        {
            renderer.Text('O', normX, normY);
        } else
        {
            renderer.Text('X', normX, normY);
        }
    }

    public override void Update(float dt)
    {
        shootTimer += dt;
        if (shootTimer < shootCooldown) return;
        shootTimer -= shootCooldown;
        shootTimer += scene.GetRandom().NextSingle() * 0.2f - 0.1f;

        Asteroid closest = scene.GetClosest<Asteroid>("Asteroid", x, y);

        located = closest!= null;

        if (located)
        {
            closest.Damage(1);

            ShootTracer tracer = new ShootTracer();
            tracer.life = 0.2f;
            tracer.x1 = x;
            tracer.y1 = y;
            tracer.x2 = closest.x;
            tracer.y2 = closest.y;
            scene.Add(tracer);
        }
    }
}
