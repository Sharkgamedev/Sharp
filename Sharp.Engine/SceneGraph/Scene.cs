using Sharp.Engine.Loading;
using System.Collections.Generic;
using Sharp.Engine.Objects;

namespace Sharp.Engine
{
    public class Scene : ILoaded
    {
        public List<GameObject> Objects = new List<GameObject>();

        public string Name;

        public string FullPath;

        public GameObject CreateObject()
        {
            GameObject obj = new ();
            obj.Load();

            Objects.Add(obj);

            return obj;
        }

        public void Load()
        {
            foreach (GameObject obj in Objects) obj.Load();
        }

        public void Update()
        {
            foreach (GameObject obj in Objects) obj.Update();
        }

        public void Render()
        {
            foreach (GameObject obj in Objects) obj.Render();
        }
    }
}
