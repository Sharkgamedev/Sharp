using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Sharp.Engine;

namespace Edge
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1280, 720),
                Title = "Edge - New Project",
                Flags = ContextFlags.ForwardCompatible,
            };

            using (Window window = new Window(GameWindowSettings.Default, nativeWindowSettings, args[0]))
            {
                window.Run();
            }
        }
    }
}
