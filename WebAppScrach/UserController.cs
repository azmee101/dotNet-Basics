// custome demo controller
/*
    A controller has 2 part
    1. Attribute
    2. Method
*/

using Microsoft.AspNetCore.Mvc;

public class UserController
{
    [HttpGet("/user")]
    public string GetUser()
    {
        return "Fetching user form controller";
    }

    [HttpPost("/anymessage")]
    public string PostUser()
    {
        return "janina";
    }
}