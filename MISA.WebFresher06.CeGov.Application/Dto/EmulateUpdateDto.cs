using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application;

public class EmulateUpdateDto
{
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
}
