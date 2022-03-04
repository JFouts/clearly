// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Clearly.Crud.RestApi;

public static class ExceptionHandler
{
    public static async Task HandleCrudApiException(HttpContext context)
    {
        context.Response.ContentType = Application.Json;
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        switch (exceptionHandlerPathFeature?.Error)
        {
            // TODO: Build out base exception for most response types
            case KeyNotFoundException:
            case FileNotFoundException:
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = "Not Found!" });
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = "Unauthorized!" });
                break;

            case AccessForbiddenException:
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = "Forbidden!" });
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Message = "Internal Server Error" });
                break;
        }
    }
}
