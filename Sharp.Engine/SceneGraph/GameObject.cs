
namespace Sharp.Engine.Objects
{
    public class GameObject : Object
    {
        public Transform transform;

        public override void AddComponent(IComponent component)
        {
            component.gameobject = this;
            base.AddComponent(component);
        }

        public override void Load()
        {
            base.Load();

            transform = new Transform();
        }
    }
}
