using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Commands
{
    public class ValidationResult
    {
        public string Message { get; private set; }
        public ErrorCode ErrorCode { get; private set; }

        public ValidationResult(ErrorCode code, string message)
        {
            this.ErrorCode = code;
            this.Message = message;
        }
    }
}
