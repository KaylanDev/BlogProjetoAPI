using Microsoft.AspNetCore.Server.HttpSys;

namespace Blog.API.ExceptionHandler;

public class Results<T>
{
    public bool IsSuccess { get; }
    public List<string>? ErrorMessage { get; } = new List<string>();
    public T? Value { get; }

    protected Results(bool isSuccess, List<string> errorMessage, T? value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage ?? new List<string>();
        Value = value;
    }

    public static Results<T> Success(T? value ) => new Results<T>(true, null, value);
    public static Results<T> Failure(string errorMessage) => new Results<T>(false, new List<string> { errorMessage }, default);
    public static Results<T> Failure(List<string> errorMessage) => new Results<T>(false, errorMessage , default);
    
}
