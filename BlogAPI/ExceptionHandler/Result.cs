namespace Blog.API.ExceptionHandler;

public class Result
{
    public bool IsSuccess { get; }
    public List<string>? ErrorMessage { get; } = new List<string>();
    public object? Value { get; }

    protected Result(bool isSuccess, string? errorMessage, object? value)
    {
        IsSuccess = isSuccess;
        if (!string.IsNullOrEmpty(errorMessage)) ErrorMessage.Add(errorMessage);
        Value = value;
    }

    public static Result Success(object? value = null) => new Result(true, null, value);
    public static Result Failure(string errorMessage)=> new Result(false, errorMessage, null);
}
