using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public  class EmulateDetailDto : EmulateDto
    {
        ///<summary>
        ///     Tên cấp khen thưởng chăng hạn như cấp nhà nước, cấp tỉnh
        ///</summary>
        public string RewarderName { get; set; }
    }
}
