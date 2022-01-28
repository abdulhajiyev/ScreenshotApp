using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ScreenshotApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Status.Content = "";
            //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        }

        private void Rec_Click(object sender, RoutedEventArgs e)
        {
            //Read Image File into Image object.
            Image img = Image.FromFile("C:\\Koala.jpg");

            //ImageConverter Class convert Image object to Byte array.
            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));

            using (TcpClient client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, 45001);
                Status.Content = "Connected to server !";

                NetworkStream nNetStream = client.GetStream();
                Image returnImage = Image.FromStream(nNetStream);

                using (var ms = new MemoryStream())
                {
                    returnImage.Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();

                    Sc.Source = bitmapImage;
                }
                //Sc.Source = returnImage;
            }
        }

        /*private void TakeScreenShot()
        {
            using var bitmap = new Bitmap(1920, 1080);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0,
                    bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            bitmap.Save("filename.jpg", ImageFormat.Jpeg);
        }*/
    }
}
