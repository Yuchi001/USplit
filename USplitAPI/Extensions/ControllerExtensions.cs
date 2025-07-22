using Microsoft.AspNetCore.Mvc;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ControllerResponse(this ControllerBase controller, ResultTuple resultTuple)
    {
        if (resultTuple.result == null) return controller.StatusCode(resultTuple.statusCode);

        return controller.Ok(resultTuple.result);
    }
}