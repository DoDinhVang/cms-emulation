using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class ErrorCode
    {
        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int Conflict = 409;
        public const int UnknowError = 500;
    }
}
