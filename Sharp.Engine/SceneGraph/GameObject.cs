
namespace Sharp.Engine.Objects
{
    public class GameObject : Object
    {
        public Transform transform;

        public override void Load()
        {
            base.Load();

            transform = new Transform();
        }
    }
}
