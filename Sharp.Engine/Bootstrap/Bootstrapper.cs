using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharp.Common.Logging;

namespace Sharp.Engine.Bootstrap
{
    /// <summary>
    /// Handles bootstrapping the engine by loading content off disk, parsing it, serializing it onto ram and loading it into the engine
    /// </summary>
    public class Bootstrapper
    {
        public Engine BootStrapEngine(string projPath)
        {
            Engine engine = new Engine();
            return LoadProject(engine, projPath);
        }

        public Engine LoadProject(Engine engine, string projPath)
        {
            using (StreamReader projectFile = File.OpenText(projPath))
            {
                JObject _projJson = (JObject)JToken.ReadFrom(new JsonTextReader(projectFile));
                Project _proj = _projJson.ToObject<Project>();

                foreach (string scenePath in _proj.Scenes)
                {
                    using (StreamReader sceneFile = File.OpenText(scenePath))
                    {
                        JObject _sceneJson = (JObject)JToken.ReadFrom(new JsonTextReader(sceneFile));
                        Scene _scene = new Scene();

                        // Set misc data
                        _scene.Name = _sceneJson["Name"].ToString();

                        foreach (JToken objData in _sceneJson["Objects"])
                        {
                            Object _object = new Object();

                            int i = 0;
                            foreach (string compName in objData["Components"])
                            {
                                Logger.Log(Type.GetType($"{compName}, Sharp.Engine.dll").ToString(), Logger.ErrorLevel.Info);

                                // Create a new component of typename defined in the object's JSON component list
                                IComponent _comp = (IComponent)Activator.CreateInstance("Sharp.Engine.dll", compName);
                                _comp.Data = objData["ComponentData"][i].ToString(); // Custom data storage for the object
                                _object.Components.Add(_comp); // Add the created component to the object
                                i++;
                            }

                            _scene.Objects.Add(_object);
                        }

                        engine.Scenes.Add(_scene);
                    }
                }
            }

            engine.ActiveScene = engine.Scenes[0];
            return engine;
        }
    }
}
