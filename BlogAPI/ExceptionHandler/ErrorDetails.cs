using System.Text.Json;

namespace Blog.API.ExceptionHandler
{
    public class ErrorDetails
    {
        
        
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            public string? Trace { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        

    }
}
