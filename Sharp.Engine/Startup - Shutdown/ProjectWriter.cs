using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Engine
{
    /// <summary>
    /// Handles storing a project file on disk
    /// </summary>
    public static class ProjectWriter
    {
        public static bool WriteStateToDisk(Engine state, string projPath)
        {
            if (!SaveProjectData(state, projPath)) return false;

            if (!SaveSceneData(state)) return false;

            return true;
        }

        private static bool SaveProjectData(Engine state, string projPath)
        {
            Project _project = new Project();
            foreach(Scene scene in state.Scenes) _project.Scenes.Add(scene.FullPath);

            using (StreamWriter file = File.CreateText(projPath + "project.sharpproj"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _project);
            }

            return true;
        }
            
        private static bool SaveSceneData(Engine state)
        {
            try
            {
                foreach (Scene scene in state.Scenes) // Foreach scene write to _scene.path it's state...
                {
                    JObject _sceneJson = new JObject(
                        new JProperty("Name", scene.Name),
                        new JProperty("Objects", scene.Objects)
                    );

                    // serialize JSON directly to a file
                    using (StreamWriter file = File.CreateText(scene.FullPath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, _sceneJson);
                    }
                }
            } catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
