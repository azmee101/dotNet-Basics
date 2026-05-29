using System.Net;
using System.Net.Sockets;
using System.Text;

public class  RequestContext
{
    public string Method {get; set;} = string.Empty;
    public string Path {get; set;} = string.Empty;
}

public class TcpServer
{
    private readonly int _port;
    private readonly Router _router;
    public TcpServer(int port, Router router)
    {
        _port = port;
        _router = router;
    }

    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Loopback, _port);
        listener.Start();

        Console.WriteLine($"Server running on port {_port}");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            await HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        // 1. Receive and parse HTTP request from client
        var stream = client.GetStream();
        var buffer = new byte[1024];
        var byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
        var requestText = Encoding.UTF8.GetString(buffer, 0, byteCount);
        
        var lines = requestText.Split("\r\n");
        var requestLine = lines[0].Split(" "); // ["GET", "/test", "HTTP/1.1"]
        
        var context = new RequestContext
        {
            Method = requestLine[0],
            Path = requestLine[1]
        };

        var responseText = _router.Resolve(context);

        // 2. Build and send HTTP response to client
        var responseBytes = Encoding.UTF8.GetBytes(
            "HTTP/1.1 200 OK\r\nContent-Length: " +
            responseText.Length + "\r\n\r\n" + responseText);
        await stream.WriteAsync(responseBytes);

        // Console.WriteLine(requestText);

        client.Close();
    }
}