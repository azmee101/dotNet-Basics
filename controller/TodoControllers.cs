using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("todo")]

public class TodoController : ControllerBase
{
    private readonly List<string> todo = new List<string>
    {
        "1. Mango",
        "2. Banana"
    };

    [HttpGet]
    public IActionResult GetTodo()
    {
        return Ok(todo);
    }

    [HttpPost("/create")]
    public IActionResult CreateTodo(Todo temp)
    {
        if(string.IsNullOrWhiteSpace(temp.Title)){
            return BadRequest("Title can not be empty");
        }
        todo.Add(temp.Title);
        return Ok(todo);
    }
}

public class Todo
{
    public string Title {get; set;} = string.Empty;
}
