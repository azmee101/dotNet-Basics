using System.Net;
using System.Net.Sockets;
using System.Text;

public class TcpServer
{
    private readonly int _port;
    public TcpServer(int port)
    {
        _port = port;
    }

    public async Task StartTcp()
    {
        var listener = new TcpListener(IPAddress.Loopback, _port);
        listener.Start();
        Console.WriteLine($"Server is running on port {_port}");
        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            await HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        var incomingData = new byte[1024];
        var messageLength = await stream.ReadAsync(incomingData);
        var clientMessage = Encoding.UTF8.GetString(incomingData, 0, messageLength);
        Console.WriteLine(clientMessage);
        client.Close(); 
    }
}