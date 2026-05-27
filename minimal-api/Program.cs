var builder = WebApplication.CreateBuilder();

var app = builder.Build();

List<string> todo = new List<string>{
    "1. Option 1",
    "2. Option 2"
};

app.MapGet("/", () => "Api Working");

app.MapGet("/todo", (HttpContext context) =>{
    return Results.Ok(todo);
});


app.MapPost("/todoquery", (string title) =>{
    if(string.IsNullOrWhiteSpace(title)){
        return Results.BadRequest("Todo title can not be empty");
    }
    todo.Add(title);
    return Results.Created($"/todo/${todo.Count-1}", title);
});

app.MapPost("/todoObject", (Todo temp) => {

    if(string.IsNullOrWhiteSpace(temp.Title)){
        return Results.BadRequest("Title ca not be empty");
    }
    todo.Add(temp.Title);
    return Results.Ok(todo);
});


app.Run();

public class Todo
{
    public string Title {get; set;} = string.Empty;
}
