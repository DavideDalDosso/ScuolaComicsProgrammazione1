using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MaterialSystem : GameObject
{
    public bool running = true;
    public float materials { get; set; }

    public override void Init(Scene scene)
    {
        this.scene = scene;
        scene.AddTag("MaterialSystem", this);
    }
    public override void Start()
    {
        
    }
    public override void Render(Renderer renderer)
    {
        float clampMats = MathF.Round(materials, 1);
        renderer.Text("Materials: " + clampMats, 215, 0);
    }

    public override void Update(float dt)
    {
        if (!running) return;
        materials += dt * 1.5f;
    }
}
