using Sharp.Engine.Loading;
using System.Collections.Generic;

namespace Sharp.Engine
{
    public class Object : ILoaded
    {
        public List<IComponent> Components = new List<IComponent>();

        public virtual void Load()
        {
            foreach (IComponent component in Components) component.Load();
        }

        public void Update()
        {
            foreach (IComponent component in Components) component.Update();
        }

        public void Render()
        {
            foreach (IComponent component in Components) component.Render();
        }
    }
}
