using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Engine.Objects
{
    public class Component : IComponent
    {
        public string Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameObject gameobject;

        public virtual void Init()
        {
        }

        public virtual void Load() { }

        public virtual void Render() { }

        public virtual void Update() { }

        public virtual Component GetComponent() => this;
    }
}
