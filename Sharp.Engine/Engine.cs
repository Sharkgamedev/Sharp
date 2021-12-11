using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Sharp.Engine.Loading;

namespace Sharp.Engine
{
    public class Engine : ILoaded
    {
        public List<Scene> Scenes = new List<Scene>();
        public static List<Shader> Shaders = new List<Shader>();
        public static string ProjectPath;

        public Scene ActiveScene;

        private Stopwatch _timer;
        private Window _window;

        public Engine (Window window, string projPath)
        {
            ProjectPath = projPath;
            _window = window;
        }

        public Engine(string projPath) => ProjectPath = projPath;

        public void Load()
        {
            if (ActiveScene == null) return;

            ActiveScene.Load();

            _timer = new Stopwatch();
            _timer.Start();
        }

        public void Render()
        {
            if (ActiveScene == null) return;
            for (int i = 0; i < Shaders.Count; i++) // Set 'global' shader uniforms
            {
                GL.Uniform1(GL.GetUniformLocation(Shaders[i].Handle, "u_time"), _timer.Elapsed.TotalSeconds);
                if (_window == null) return;
                GL.Uniform2(GL.GetUniformLocation(Shaders[i].Handle, "u_resolution"), _window.Size);
                GL.Uniform2(GL.GetUniformLocation(Shaders[i].Handle, "u_mouse"), _window.MouseState.Position);
            }

            ActiveScene.Render();
        }

        public void Update()
        {
            if (ActiveScene == null) return;

            ActiveScene.Update();
        }

        public static void RegisterShader(Shader shader) => Shaders.Add(shader);
    }
}
