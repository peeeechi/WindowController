using System;

namespace WindowController
{
    public class WinApiException: Exception
    {
        public uint ErrorCode { get; }
        public WinApiException(uint errorCode, string message): base(message)
        {
            this.ErrorCode = errorCode;
        }
    }    
}