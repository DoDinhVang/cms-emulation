using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public class PagingInfoDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string? KeyWord { get; set; }
        public string? RewardObj { get; set; }
        public string? RewardType { get; set; }
        public Guid? RewarderId { get; set; }
        public int? Status { get; set; }
    }
}
