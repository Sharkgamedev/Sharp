using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace Sharp.Engine
{
    class Texture
    {
        public readonly int Handle;

        private string _texturePath;

        public Texture(string texturePath)
        {
            if (!File.Exists(texturePath)) throw new Exception($"Unable to find the texture located at {texturePath}", new DirectoryNotFoundException($"No texture at {texturePath}"));
            _texturePath = texturePath;

            Handle = GL.GenTexture();
        }

        public Texture(string name, int width, int height, IntPtr data, bool generateMips = false, bool srgb = false)
        {
            SizedInternalFormat intFormat = srgb ? (SizedInternalFormat)All.Srgb8Alpha8 : SizedInternalFormat.Rgba8;
            int mipLevels = generateMips == false ? 1 : (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

            Handle = GL.GenTexture();
            GL.TextureStorage2D(Handle, mipLevels, intFormat, width, height);

            GL.TextureSubImage2D(Handle, 0, 0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, data);

            if (generateMips) GL.GenerateTextureMipmap(Handle);

            GL.TextureParameter(Handle, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TextureParameter(Handle, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TextureParameter(Handle, TextureParameterName.TextureMaxLevel, mipLevels - 1);
        }

        [SupportedOSPlatform("windows")]
        public void LoadTexture()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            using (Bitmap image = new Bitmap(_texturePath))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);

                System.Drawing.Imaging.BitmapData data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
            }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
