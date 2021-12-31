using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharp.Common.Logging;
using Sharp.Engine.Objects;

namespace Sharp.Engine.Bootstrap
{
    /// <summary>
    /// Handles bootstrapping the engine by loading content off disk, parsing it, serializing it onto ram and loading it into the engine
    /// </summary>
    public static class Bootstrapper
    {
        public static Engine BootStrapEngine(string projPath)
        {
            Engine engine = new Engine(projPath);
            return LoadProject(engine, projPath);
        }

        public static Engine LoadProject(Engine engine, string projPath)
        {
            if (File.Exists(projPath + "project.sharpproj"))
            {
                using (StreamReader projectFile = File.OpenText(projPath + "project.sharpproj"))
                {
                    JObject _projJson = (JObject)JToken.ReadFrom(new JsonTextReader(projectFile));
                    Project _proj = _projJson.ToObject<Project>();
                    
                    foreach (string scenePath in _proj.Scenes)
                    {
                        if (File.Exists(scenePath))
                        {
                            using (StreamReader sceneFile = File.OpenText(scenePath))
                            {
                                JObject _sceneJson = (JObject)JToken.ReadFrom(new JsonTextReader(sceneFile));
                                Scene _scene = new Scene();

                                // Set misc data
                                _scene.Name = _sceneJson["Name"].ToString();
                                _scene.FullPath = scenePath;

                                foreach (JToken objData in _sceneJson["Objects"])
                                {
                                    GameObject _object = new GameObject();

                                    int i = 0;
                                    foreach (string compName in objData["Components"])
                                    {
                                        Logger.Log(Type.GetType($"{compName}, Sharp.Engine.dll").ToString(), Logger.ErrorLevel.Info);

                                        // Create a new component of typename defined in the object's JSON component list
                                        IComponent _comp = (IComponent)Activator.CreateInstance("Sharp.Engine.dll", compName);
                                        _comp.Data = objData["ComponentData"][i].ToString(); // Custom data storage for the object
                                        _comp.GetComponent().gameobject = _object; // Link the created componet to a GameObject
                                        _object.Components.Add(_comp); // Add the created component to the object
                                        i++;
                                    }

                                    _scene.Objects.Add(_object);
                                }

                                engine.Scenes.Add(_scene);
                            }
                        }
                    }
                    
                }
            }

            if (engine.Scenes.Count < 1) engine.Scenes.Add(new Scene() { FullPath = Engine.ProjectPath + "Scenes\\scene.sharp" });

            engine.ActiveScene = engine.Scenes[0];
            return engine;
        }
    }
}
