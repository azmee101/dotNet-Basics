var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/codecamp", (requestContext) =>
{
    Console.WriteLine("Hello world!");
    return "anything";
});

app.MapPost("/query", (requestContext) =>
{
   return "sample"; 
});

app.MapControllers();

await app.RunAsync(5007);