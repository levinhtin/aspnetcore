using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public enum OperationStatus { Success, Failure }

    public class OperationResult<T>
    {
        public T data { get; private set; }

        public OperationStatus status { get; private set; }

        public string message { get; private set; }

        public ResultError error { get; set; }
        public bool IsSuccess
        {
            get
            {
                return this.status == OperationStatus.Success;
            }
        }

        public OperationResult(OperationStatus status, string message, T data)
        {
            this.data = data;
            this.status = status;
            this.message = message;
        }

        public OperationResult(OperationStatus status, string errorCode, string errorMessage)
        {
            this.data = data;
            this.status = status;
            this.message = "Error";
            this.error = new ResultError() { code = errorCode, message = errorMessage };
        }

        public static implicit operator bool(OperationResult<T> result)
        {
            return result.IsSuccess;
        }
    }
    public class Meta
    {
        public string copyright { get; set; }
        public string author { get; set; }
    }
    public class ResultError
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
