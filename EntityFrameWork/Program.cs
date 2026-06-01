using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// postgresql connection string and dependency injection setup for AppDbContext
var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=codecamp_db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapPost("/user", async (AppDbContext context, UserContext req) =>
{
    var user = new User
    {  // Id dicchina, caz entity framework nije nijei ID k populate kore nibe,
       // caz entity framework ID dekhlei dhore ney this is a primary key
        Name = req.Name,
        Email = req.Email
    };
    await context.Users.AddAsync(user);
    await context.SaveChangesAsync();
});

app.Run();