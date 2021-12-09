using Sharp.Engine.Loading;
using System.Collections.Generic;

namespace Sharp.Engine
{
    public class Scene : ILoaded
    {
        public List<Object> Objects = new List<Object>();

        public string Name;

        public string FullPath;

        public void Load()
        {
            foreach (Object obj in Objects) obj.Load();
        }

        public void Update()
        {
            foreach (Object obj in Objects) obj.Update();
        }

        public void Render()
        {
            foreach (Object obj in Objects) obj.Render();
        }
    }
}
