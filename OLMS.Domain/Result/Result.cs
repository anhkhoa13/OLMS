namespace OLMS.Domain.Result;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; set; }
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T> : Result
{
    public T? Value { get; }
    private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    private Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = default;
    }

    public static Result<T> Success(T value) => new (value, true, Error.None);
    public static new Result<T> Failure(Error error) => new(false, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
