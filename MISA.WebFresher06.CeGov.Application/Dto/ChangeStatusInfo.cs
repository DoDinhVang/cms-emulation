using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public class ChangeStatusInfo
    {
        public Guid Id {get; set;}
        public EmulateStatus Status { get; set; }
    }
}
