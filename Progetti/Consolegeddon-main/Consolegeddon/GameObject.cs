using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class GameObject
{
    protected Scene? scene;
    public float x;
    public float y;
    public List<string> tags = new List<string>();
    public virtual void Init(Scene scene)
    {
        this.scene = scene;
    }
    public abstract void Start();
    public abstract void Update(float dt);
    public abstract void Render(Renderer renderer);
}
