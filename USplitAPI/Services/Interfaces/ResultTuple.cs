namespace USplitAPI.Services.Interfaces;

public record ResultTuple
{
    public readonly object? result;
    public readonly int statusCode;

    private ResultTuple(object result)
    {
        this.result = result;
        statusCode = StatusCodes.Status200OK;
    }

    private ResultTuple(int statusCode)
    {
        this.result = null;
        this.statusCode = statusCode;
    }

    public static ResultTuple Success(object res)
    {
        return new ResultTuple(res);
    }

    public static ResultTuple Exception(int code)
    {
        return new ResultTuple(code);
    }
}