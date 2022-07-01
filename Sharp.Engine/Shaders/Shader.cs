using System;
using System.IO;

namespace Sharp.Engine
{
    public class Shader
    {
        public int Handle;

        private int _vertexShader;
        private int _fragmentShader;

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {


            Engine.RegisterShader(this);
        }

        private void CreateShader()
        {

        }

        public void Use()
        {
            
        }

        public int GetUniform(string name) { return 0; }
    }
}
