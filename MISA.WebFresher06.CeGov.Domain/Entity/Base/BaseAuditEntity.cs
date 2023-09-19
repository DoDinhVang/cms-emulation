using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain.Entity
{
    public abstract class BaseAuditEntity
    {
        ///<summary>
        ///     Ngày tạo danh hiệu thi đua
        ///</summary>
        public DateTime? CreatedDate { get; set; }
        ///<summary>
        ///     Người tạo danh hiệu thi đua
        ///</summary>
        public string? CreatedBy { get; set; }
        ///<summary>
        ///   Ngày sửa thông tin danh hiệu thi  đua
        ///</summary>
        public DateTime? ModifiedDate { get; set; }
        ///<summary>
        ///   Người sưa dữ liệu gần nhất
        ///</summary>
        public string? ModifiedBy { get; set; }
    }
}
