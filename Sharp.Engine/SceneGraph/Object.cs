using Sharp.Engine.Loading;
using System.Collections.Generic;

namespace Sharp.Engine
{
    public class Object : ILoaded
    {
        private List<IComponent> _components = new List<IComponent>();

        public void Load()
        {
            foreach (IComponent component in _components) component.Load();
        }

        public void Update()
        {
            foreach (IComponent component in _components) component.Update();
        }

        public void Render()
        {
            foreach (IComponent component in _components) component.Render();
        }
    }
}
