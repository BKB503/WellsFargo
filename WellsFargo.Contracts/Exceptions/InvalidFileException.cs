using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Contracts.Exceptions
{
    public class InvalidFileException : Exception
    {

        public InvalidFileException() : base()
        {
        }

        public InvalidFileException(string message) : base(message)
        {
        }

        public InvalidFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
