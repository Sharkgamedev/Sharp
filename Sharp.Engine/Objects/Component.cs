using System;

namespace Sharp.Engine.Objects
{
    public class Component : IComponent
    {
        public string Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameObject gameobject { get; set; }

        public virtual void Init()
        {
        }

        public virtual void Load() { }

        public virtual void Render() { }

        public virtual void Update() { }

        public virtual Component GetComponent() => this;
    }
}
