namespace USplitAPI.Helpers;

public static class ResultTupleExtensions
{
    public static async Task<T> ExtractResultAsync<T>(this Task<ResultTuple> resultTask)
    {
        var resultTuple = await resultTask;
        return resultTuple.Result<T>();
    }
}