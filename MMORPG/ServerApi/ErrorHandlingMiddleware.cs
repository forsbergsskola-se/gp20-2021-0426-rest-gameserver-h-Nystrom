﻿using System;
using Microsoft.AspNetCore.Mvc;
using MMORPG.ServerApi.ServerExceptions;

namespace MMORPG.ServerApi{
    public static class ErrorHandlingMiddleware{
        public static IActionResult GetHttpCodeStatus(this Exception exception){
            return exception switch{
                NotFoundException => new StatusCodeResult((int) ErrorCode.NotFound),
                RequestTimeoutException => new StatusCodeResult((int) ErrorCode.ServerTimeOut),
                _ => new StatusCodeResult((int) ErrorCode.BadRequest)
            };
        }
        enum ErrorCode{
            BadRequest = 400,
            Unauthorized = 401,
            NotFound = 404,
            ServerTimeOut = 408,
        }
    }
}