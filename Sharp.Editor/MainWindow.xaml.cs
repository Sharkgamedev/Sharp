using System.Windows;

namespace Sharp.Editor
{
    /// <summary>
    /// Startup point for the program
    /// </summary>
    public partial class MainWindow : Window
    {
        // Cannot explicitly include Sharp.Engine beccause it contains it's own implementation of 'Window'
        public Engine.Engine Engine;

        public MainWindow()
        {
            InitializeComponent();

            Engine = new Engine.Engine();
            Engine.Load();

            View.Start(new OpenTK.Wpf.GLWpfControlSettings 
            {
                MajorVersion = 4,
                MinorVersion = 6
            });
        }

        private void View_Render(System.TimeSpan obj)
        {
            Engine.Render();
        }
    }
}
