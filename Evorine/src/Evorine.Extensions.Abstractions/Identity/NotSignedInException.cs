using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public class NotSignedInException : UnauthorizedAccessException
    {
        public NotSignedInException()
            : base("Sign-in required to proceed!")
        {

        }

        public NotSignedInException(string message)
            : base(message)
        {

        }
    }
}
