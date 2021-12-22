using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using Sharp.Common.Logging;

namespace Sharp.Engine
{
    public class Shader
    {
        public int Handle;

        private int _vertexShader;
        private int _fragmentShader;

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            CreateShader(ShaderType.VertexShader, vertexShaderPath, out _vertexShader);
            CreateShader(ShaderType.FragmentShader, fragmentShaderPath, out _fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, _vertexShader);
            GL.AttachShader(Handle, _fragmentShader);
            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int code);
            if (code != (int)All.True)
            {
                Logger.Log($"Error occurred during Shader Program Linking ({Handle})", Logger.ErrorLevel.Error);
                throw new Exception($"Error occurred during Shader Program Linking ({Handle})");
            }

            GL.DetachShader(Handle, _vertexShader);
            GL.DetachShader(Handle, _fragmentShader);
            GL.DeleteShader(_vertexShader);
            GL.DeleteShader(_fragmentShader);

            Engine.RegisterShader(this);
        }

        private void CreateShader(ShaderType type, string path, out int handle)
        {
            string source = File.ReadAllText(path);

            handle = GL.CreateShader(type);
            GL.ShaderSource(handle, source);

            GL.CompileShader(handle);
            GL.GetShader(handle, ShaderParameter.CompileStatus, out int code);
            if (code != (int)All.True)
            {
                string infoLog = GL.GetShaderInfoLog(handle);
                Logger.Log($"Error occurred during Shader compilation ({handle}).\n\n{infoLog}", Logger.ErrorLevel.Error);
                throw new Exception($"Error occurred during Shader compilation ({handle}).\n\n{infoLog}");
            }
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }
    
        public int GetUniform(string name) => GL.GetUniformLocation(Handle, name);
    }
}
