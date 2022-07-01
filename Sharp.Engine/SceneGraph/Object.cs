using Sharp.Engine.Loading;
using System.Collections.Generic;
using System.Linq;

namespace Sharp.Engine
{
    public class Object : ILoaded
    {
        public List<IComponent> Components = new List<IComponent>();

        public virtual void AddComponent(IComponent component)
        {
            component.Init();
            component.Load();
            Components.Add(component);
        }

        public virtual T GetComponent<T>()
        {
            foreach (IComponent component in Components) if (component.GetType() == typeof(T)) return (T)component;
            return default;
        }

        public virtual List<T> GetComponents<T>() => Components.Where(component => component.GetType() == typeof(T)).Select(component => (T)component).ToList();

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
