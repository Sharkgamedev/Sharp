using System.Diagnostics;
using System.Windows;
using Sharp.Engine;
using Sharp.Engine.Components;

namespace Sharp.Editor
{
    /// <summary>
    /// Startup point for the program
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        // Cannot explicitly include Sharp.Engine beccause it contains it's own implementation of 'Window'
        public Engine.Engine Engine;
        public string ProjectPath = @"E:\Documents\Random Projects\Test\";

        public MainWindow()
        {
            InitializeComponent();

            Engine = Sharp.Engine.Bootstrap.Bootstrapper.BootStrapEngine(ProjectPath);
            Engine.Load();

            Debug.WriteLine("f");

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Object sceneObj = new Object();
            sceneObj.Components.Add(new SpriteRenderer());

            Engine.ActiveScene.Objects.Add(sceneObj);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ProjectWriter.WriteStateToDisk(Engine, ProjectPath);
        }
    }
}
