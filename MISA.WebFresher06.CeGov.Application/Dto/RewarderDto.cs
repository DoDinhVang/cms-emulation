using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application.Dto
{
    public class RewarderDto
    {
        /// <summary>
        ///     Id Cấp khen thưởng
        /// </summary>
        public Guid RewarderId { get; set; }
        /// <summary>
        ///  Tên cấp khen thưởng
        /// </summary>
        public string RewarderName { get; set; }
    }
}
