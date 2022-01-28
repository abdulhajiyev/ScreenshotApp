using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;


var ip = IPAddress.Loopback;
var listener = new TcpListener(ip, 45001);

listener.Start(100);
Console.WriteLine("Listening on {0}", listener.LocalEndpoint);

/*using var bitmap = new Bitmap(1920, 1080);
using (var g = Graphics.FromImage(bitmap))
{
    g.CopyFromScreen(0, 0, 0, 0,
        bitmap.Size, CopyPixelOperation.SourceCopy);
}

bitmap.Save("screenshot.jpg", ImageFormat.Jpeg);*/

while (true)
{
    listener.AcceptTcpClient();
    Console.WriteLine("Client connected");
    // Send image to client
    var stream = new MemoryStream();

    using var bitmap = new Bitmap(1920, 1080);
    using (var g = Graphics.FromImage(bitmap))
    {
        g.CopyFromScreen(0, 0, 0, 0,
            bitmap.Size, CopyPixelOperation.SourceCopy);
    }

    bitmap.Save("screenshot.jpg", ImageFormat.Jpeg);
    bitmap.Save(stream, ImageFormat.Jpeg);
    var bytes = stream.ToArray();
    var client = listener.AcceptTcpClient();
    var stream2 = client.GetStream();
    stream2.Write(bytes, 0, bytes.Length);
    stream2.Close();
    client.Close();
}


/*
var ip = IPAddress.Loopback;
var listener = new TcpListener(ip, 45001);

listener.Start(100);
Console.WriteLine("Listening on {0}", listener.LocalEndpoint);

while (true)
{
    listener.AcceptTcpClient();
    Console.WriteLine("Client connected");

    var client = listener.AcceptTcpClient();
    var stream = client.GetStream();
    var binaryWriter = new BinaryWriter(stream);
    var binaryReader = new BinaryReader(stream);

    Image img = Image.FromFile(@"C:\Users\Raider\source\repos\ScreenshotApp\ScreenshotServer\bin\Debug\net6.0\filename.jpg");
    byte[] arr;
    using (MemoryStream ms = new MemoryStream())
    {
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        arr = ms.ToArray();
    }

    while (true)
    {
        var imgToSend = binaryReader.ReadString();
        Console.WriteLine(message);
        binaryWriter.Write("Hakuna Matata");
    }
}*/