using MISA.WebFresher06.CeGov.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class Rewarder : BaseAuditEntity, IEntity<Guid>
    {
        /// <summary>
        ///     Id Cấp khen thưởng
        /// </summary>
        public Guid RewarderId { get; set; }
        /// <summary>
        ///  Tên cấp khen thưởng
        /// </summary>
        public string RewarderName { get; set; }

        public Guid GetId()
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
