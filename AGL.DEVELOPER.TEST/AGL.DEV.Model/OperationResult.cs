using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.DEV.Model
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }

        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }       
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool isSuccess) : base(isSuccess) { }

        public T Entity { get; set; }
    }
}
