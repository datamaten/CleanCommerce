using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions;

public class ApiGuardException : Exception
{
    public ApiGuardException() { }

    public ApiGuardException(string message)
        : base(message)
    {
    }

    public ApiGuardException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
