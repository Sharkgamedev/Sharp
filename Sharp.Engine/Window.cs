using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Sharp.Engine
{
    public class Window : GameWindow
    {
        public Engine Engine;
        public string ProjectPath;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string projPath) : base(gameWindowSettings, nativeWindowSettings) => ProjectPath = projPath;

        protected virtual void Init()
        {
            Engine = new Engine(this, ProjectPath);
            Engine.ActiveScene = new Scene();
        }

        protected override void OnLoad()
        {
            Init();
            base.OnLoad();

            Engine.Load();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            Engine.Render();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

            Engine.Update();

            base.OnUpdateFrame(args);
        }
    }
}
