using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;

var client = new TcpClient();

var ip = IPAddress.Loopback;
var listener = new TcpListener(ip, 45001);

listener.Start(100);
Console.WriteLine("Listening on {0}", listener.LocalEndpoint);


while (true)
{
    listener.AcceptTcpClient();
    Console.WriteLine("Client connected");
    // take screenshot
    var bmp = new Bitmap(1920, 1080);
    var gfx = Graphics.FromImage(bmp);
    gfx.CopyFromScreen(0, 0, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
    // send screenshot to client with binary writer
    // don't use client.GetStream() because it's not working

}

    // Send image to client
    /*using var bitmap = new Bitmap(1920, 1080);
    using (var g = Graphics.FromImage(bitmap))
    {
        g.CopyFromScreen(0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
    }

    bitmap.Save("screenshot.jpg", ImageFormat.Jpeg);*/