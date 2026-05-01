namespace SharedService.Returns;

public class Result<T>
{
    public string Message { get; private set; }
    public int Code { get; private set; }
    public T? Data { get; private set; }
    public bool IsOkResult { get; private set; }

    private Result() { }

    private Result(string message, int code, T? data, bool isOkResult)
    {
        Message = message;
        Code = code;
        Data = data;
        IsOkResult = isOkResult;
    }

    private Result(Result<T> result)
    {
        Message = result.Message;
        Code = result.Code;
        Data = result.Data;
        IsOkResult = result.IsOkResult;
    }

    public static Result<T> SuccessResult(string message, int code, T data) =>
        new Result<T>(message, code, data, true);

    public static Result<T> SuccessResult(string message, int code) =>
        new Result<T>(message, code, default(T), true);

    public static Result<T> FailureResult(string message, int code) =>
        new Result<T>(message, code, default(T), false);

    public static Result<T> SuccessResult(Result<T> result) =>
        new Result<T>(result);

    public static Result<T> FailureResult(Result<T> result) =>
        new Result<T>(result);

}
