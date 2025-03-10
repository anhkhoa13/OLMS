namespace OLMS.Domain.Result;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public Error Error { get; set;}
    private Result(T value, bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }
    private Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result<T> Success(T value) => new(value, true, Error.None);
    public static Result<T> Failure(Error error) => new(false, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
