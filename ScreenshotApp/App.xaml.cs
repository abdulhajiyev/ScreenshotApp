using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace ScreenshotApp
{
    public partial class App : Application
    {
        public void TakeSs()
        {
            using var bitmap = new Bitmap(1920, 1080);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0,
                    bitmap.Size, CopyPixelOperation.SourceCopy);
            }

            bitmap.Save("filename.jpg", ImageFormat.Jpeg);
        }
    }
}