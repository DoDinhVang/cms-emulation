using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class ExcelUploadParameters
    {
        public string? EmulateName { get; set; }
        public string? EmulateCode { get; set; }
        public string? RewardObj { get; set; }
        public string? RewardType { get; set; }
        public Guid RewarderId { get; set; }
        public EmulateStatus? Status { get; set; }
    }
}
