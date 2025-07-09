using Microsoft.AspNetCore.Server.HttpSys;

namespace Blog.API.ExceptionHandler;

public class Result<T>
{
    public bool IsSuccess { get; }
    public List<string>? ErrorMessage { get; } = new List<string>();
    public T? Value { get; }

    protected Result(bool isSuccess, List<string> errorMessage, T? value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage ?? new List<string>();
        Value = value;
    }

    public static Result<T> Success(T? value ) => new Result<T>(true, null, value);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, new List<string> { errorMessage }, default);
    public static Result<T> Failure(List<string> errorMessage) => new Result<T>(false, errorMessage , default);
    
}
