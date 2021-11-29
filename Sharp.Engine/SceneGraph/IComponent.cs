using Sharp.Engine.Loading;

namespace Sharp.Engine
{
    interface IComponent : ILoaded
    {
        public void Init();
    }
}
