Router route = new Router();

route.MapGet("/codecamp", (RequestContext) =>
{
    Console.WriteLine("Hello World!");
    return "anything";
});

var server = new TcpServer(5005, route);
await server.StartAsync();