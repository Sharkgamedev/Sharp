using Sharp.Engine.Loading;

namespace Sharp.Engine
{
    public interface IComponent : ILoaded
    {
        public string Data { get; set; }

        public void Init();
    }
}
