using System;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;

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
        }

        public Texture(string name, int width, int height, IntPtr data, bool generateMips = false, bool srgb = false)
        {

        }

        [SupportedOSPlatform("windows")]
        public void LoadTexture()
        {
            
            using (Bitmap image = new Bitmap(_texturePath))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);

                System.Drawing.Imaging.BitmapData data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

               
            }

        }

        public void Use()
        {

        }
    }
}
