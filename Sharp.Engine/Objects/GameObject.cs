using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Engine.Objects
{
    public class GameObject : IComponent
    {
        public string Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Transform transform;

        public virtual void Init()
        {
            transform = new Transform();
        }

        public virtual void Load() { }

        public virtual void Render() { }

        public virtual void Update() { }
    }
}
