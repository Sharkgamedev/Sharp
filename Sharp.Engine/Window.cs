using Windows.Win32.Foundation;
using static Windows.Win32.PInvoke;

namespace Sharp.Engine.Windowing
{
    public struct WindowInfo
    {
        public string Title;
    }

    public class Window
    {
        private HWND _pointer;

        public Window(WindowInfo Info)
        {
           // _pointer = CreateWindowEx(
             //   Windows.Win32.UI.WindowsAndMessaging.WINDOW_EX_STYLE.WS_EX_APPWINDOW
             //   Info.Title
             //   )
        }
    }
}
