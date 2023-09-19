using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application;

public class EmulateDto
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
    ///   Người sửa dữ liệu gần nhất
    ///</summary>
    public string? ModifiedBy { get; set; }
}
