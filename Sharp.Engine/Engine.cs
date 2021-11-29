using System.Collections.Generic;
using Sharp.Engine.Loading;

namespace Sharp.Engine
{
    public class Engine : ILoaded
    {
        public List<Scene> Scenes = new List<Scene>();

        public Scene ActiveScene;

        public void Load()
        {
            if (ActiveScene == null) return;

            ActiveScene.Load();
        }

        public void Render()
        {
            if (ActiveScene == null) return;

            ActiveScene.Render();
        }

        public void Update()
        {
            if (ActiveScene == null) return;

            ActiveScene.Update();
        }
    }
}
