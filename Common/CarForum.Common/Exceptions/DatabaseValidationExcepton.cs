using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Common.Exceptions
{
    public class DatabaseValidationExcepton : Exception
    {
        public DatabaseValidationExcepton()
        {
        }

        public DatabaseValidationExcepton(string? message) : base(message)
        {
        }

        public DatabaseValidationExcepton(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DatabaseValidationExcepton(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
