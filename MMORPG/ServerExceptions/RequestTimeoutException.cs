using System;

namespace MMORPG.ServerApi.ServerExceptions{
    public class RequestTimeoutException : Exception{
        public RequestTimeoutException() { }
        public RequestTimeoutException(string message)
            : base(message) { }

        public RequestTimeoutException(string message, Exception inner)
            : base(message, inner) { }
    }
}