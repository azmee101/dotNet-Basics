// attribute gulo deal korar onno ei file
// attribute gulo chenar jonno ei file
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]

public abstract class HttpMethodAttribute : Attribute
{
    public string Path {get;}
    public string Method {get;}

    protected HttpMethodAttribute(string path, string method)
    {
        Path = path;
        Method = method;
    }
}

public sealed class HttpGetAttribute : HttpMethodAttribute
{
    public HttpGetAttribute(string path) : base(path, "GET"){}
}

public sealed class HttpPostAttribute : HttpMethodAttribute
{
    public HttpPostAttribute(string path) : base(path, "GET"){}
}