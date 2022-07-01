using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Sharp.Engine;
using Sharp.Engine.Components;
using Sharp.Engine.Objects;

namespace ProjectCD
{
    internal class Game : Window
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string projPath) : base(gameWindowSettings, nativeWindowSettings, projPath) { }

        protected override void OnLoad()
        {
            base.OnLoad();
            GameObject robot = Engine.ActiveScene.CreateObject();
            robot.AddComponent(new SpriteRenderer());

            OnLoadContent();
        }

        protected void OnLoadContent()
        {

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

        }
    }
}
