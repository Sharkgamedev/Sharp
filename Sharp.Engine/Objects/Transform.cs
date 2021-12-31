using OpenTK.Mathematics;

namespace Sharp.Engine.Objects
{
    public class Transform
    {
        public Vector3 position;
        public Quaternion quaternion;
        public Vector3 scale;

        public Vector3 eulerAngles { get => quaternion.ToEulerAngles(); set => quaternion =  Quaternion.FromEulerAngles(value); }

        public Transform(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            this.position = position;
            this.quaternion = quaternion;
            this.scale = scale;
        }

        public Transform()
        {
            position = Vector3.Zero;
            quaternion = new Quaternion(Vector3.Zero);
            scale = Vector3.Zero;
        }
    }
}
