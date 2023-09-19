using MISA.WebFresher06.CeGov.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public class Emulate : BaseAuditEntity, IEntity<Guid>
    {
        /// <summary>
        ///     Id của danh hiệu thi đua
        /// </summary>
        public Guid EmulateId { get; set; }
        /// <summary>
        ///   Tên danh hiệu
        /// </summary>
        public string EmulateName { get; set; }
        /// <summary>
        ///     Mã danh hiệu thi đua
        /// </summary>
        public string EmulateCode { get; set; }
        ///<summary>
        ///     Đối tượn khen thưởng
        ///</summary>
        public string RewardObj { get; set; }
        ///<summary>
        ///     Loại phong trào
        ///</summary>
        public string RewardType { get; set; }
        ///<summary>
        ///     ID cấp khen thưởng
        ///</summary>
        public Guid RewarderId { get; set; }
        ///<summary>
        ///     Mổ tả.Mô tả bổ sung thông thin về danh hiệu thi đua
        ///</summary>
        public string? Description { get; set; }
        ///<summary>
        ///     Trạng thái danh hiệu
        ///</summary>
        public EmulateStatus Status { get; set; }

        public Guid GetId()
        {
            return EmulateId;
        }

        public void SetId(Guid id)
        {
            EmulateId = id;
        }
    }
}
