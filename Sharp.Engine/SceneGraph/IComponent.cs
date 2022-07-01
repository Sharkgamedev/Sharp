using Sharp.Engine.Loading;
using Sharp.Engine.Objects;

namespace Sharp.Engine
{
    public interface IComponent : ILoaded
    {
        public string Data { get; set; }
        public GameObject gameobject { get; set; }

        public void Init();

        public Component GetComponent();
    }
}
