using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application;

public class EmulateCreateDto
{
    [Required(ErrorMessage = "Tên danh hiệu không được bỏ trống")]
    public string EmulateName { get; set; }
    /// <summary>
    ///     Mã danh hiệu thi đua
    /// </summary>
    [Required(ErrorMessage = "Mã danh hiệu không được bỏ trống")]
    public string EmulateCode { get; set; }
    ///<summary>
    ///     Đối tượn khen thưởng
    ///</summary>
    [Required(ErrorMessage = "Đối tượng khen thương không được bỏ trống")]
    ///
    public string RewardObj { get; set; }
    ///<summary>
    ///     Loại phong trào
    ///</summary>
    [Required(ErrorMessage = "Loại phong trào không được để trống")]

    public string RewardType { get; set; }
    ///<summary>
    ///     ID cấp khen thưởng
    ///</summary>
    [Required(ErrorMessage = "Cấp khen thưởng không được bỏ trống")]
    public Guid RewarderId { get; set; }
    ///<summary>
    ///     Mổ tả.Mô tả bổ sung thông thin về danh hiệu thi đua
    ///</summary>
    public string ? Description { get; set; }
}
