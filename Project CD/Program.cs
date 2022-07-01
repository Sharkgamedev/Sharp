using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Sharp.Engine;

namespace ProjectCD
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            NativeWindowSettings _nativeWindowSettings = new()
            {
                Size = new Vector2i(1280, 720),
                Title = "Project CD",
                Flags = ContextFlags.ForwardCompatible,
            };

            using (Game window = new (GameWindowSettings.Default, _nativeWindowSettings, "Content"))
            {
                window.Run();
            }
        }
    }
}
