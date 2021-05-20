using System;
using Microsoft.AspNetCore.Mvc;
using MMORPG.ServerApi.ServerExceptions;

namespace MMORPG.ServerApi{
    public static class ErrorHandlingMiddleware{
        public static IActionResult GetHttpCodeStatus(this Exception exception){
            var statusCode = exception switch{
                NotFoundException => (int) ErrorCode.NotFound,
                RequestTimeoutException => (int)ErrorCode.ServerTimeOut,
                _ => (int)ErrorCode.BadRequest
            };
            return new ContentResult{Content = exception.Message, StatusCode = statusCode};
        }

        enum ErrorCode{
            BadRequest = 400,
            Unauthorized = 401,
            NotFound = 404,
            ServerTimeOut = 408,
        }
    }
}