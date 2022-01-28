using System.Drawing;
using System.Net;
using System.Net.Sockets;

var ip = IPAddress.Loopback;
var client = new TcpClient();
client.Connect(ip, 45001);
var stream = client.GetStream();
var binaryReader = new BinaryReader(stream);


// Receive image from server
var image = new Bitmap(binaryReader.BaseStream);
// Write image to file
image.Save("image.png");
// Close connection
client.Close();

