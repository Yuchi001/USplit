namespace USplitAPI.Helpers;

public record ResultTuple
{
    public readonly object? result;
    public readonly int statusCode;
    public readonly string message;

    private ResultTuple(object result)
    {
        this.message = "200OK";
        this.result = result;
        statusCode = StatusCodes.Status200OK;
    }

    private ResultTuple(int statusCode, string message)
    {
        this.message = message;
        this.result = null;
        this.statusCode = statusCode;
    }

    public T Result<T>() => (T)result!;

    public static ResultTuple Success(object res)
    {
        return new ResultTuple(res);
    }

    public static ResultTuple Exception(int code, string? message = null)
    {
        return new ResultTuple(code, message ?? code.ToString());
    }
}