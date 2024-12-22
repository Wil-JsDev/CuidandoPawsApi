using CuidandoPawsApi.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Utils
{
    public class Error
    {

        private Error(
            string code,
            string description,
            ErrorType errorType
            )
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { get; }

        public string Description { get; }

        public ErrorType ErrorType { get; }

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

    }
}
