using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IGameObject
{
    public void Update(float dt, Scene scene);
    public void Render(Renderer renderer);
}
