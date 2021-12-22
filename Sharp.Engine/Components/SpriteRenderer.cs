using OpenTK.Graphics.OpenGL4;
using Sharp.Engine.Objects;
using System;

namespace Sharp.Engine.Components
{
    public class SpriteRenderer : GameObject
    {
        private const string c_shaderLoc = "Shaders/simple_sprite";

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

            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();
            _shader = new Shader($"{c_shaderLoc}.frag", $"{c_shaderLoc}.vert");
            _texture = new Texture("Textures/Robot.png");
        }

        public override void Load()
        {
            _texture.LoadTexture();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * r_verts.Length, r_verts, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(_vao);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, true, sizeof(float) * 4, 0);

            // Unbind
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public override void Render()
        {
            GL.Uniform3(_shader.GetUniform("u_position"), ref transform.position);
            
            _shader.Use();
            _texture.Use(TextureUnit.Texture0);

            // Unbind
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, r_verts.Length);
            GL.BindVertexArray(0);
        }

        public override void Update()
        {

        }
    }
}
