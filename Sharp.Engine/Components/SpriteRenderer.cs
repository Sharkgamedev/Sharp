using Sharp.Engine.Objects;
using System;

namespace Sharp.Engine.Components
{
    public class SpriteRenderer : Component
    {
#if DEBUG
        private string c_shaderLoc = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "../../../../Sharp.Common/Shaders/simple_sprite";
#else
        private const string c_shaderLoc = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "../Shaders/simple_sprite";
#endif

        private int _vao;
        private int _vbo;
        private Shader _shader;
        private Texture _texture;

        private readonly float[] r_verts = 
            { 
                // 2P, 2T
                1.0f, 0.0f, 1.0f, 0.0f,
                1.0f, 1.0f, 1.0f, 1.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                // Triangle 2
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 1.0f,
                1.0f, 0.0f, 1.0f, 0.0f
            };

        public override void Init()
        {
            base.Init();

            _shader = new Shader($"{c_shaderLoc}.frag", $"{c_shaderLoc}.vert");
            _texture = new Texture("Textures/Robot.png");
        }

        public override void Load()
        {
            _texture.LoadTexture();

        }

        public override void Render()
        {
            
            _shader.Use();
        }

        public override void Update()
        {

        }
    }
}
