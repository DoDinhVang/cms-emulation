using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace MISA.WebFresher06.CeGov.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class EmulatesController : BaseCrudController<EmulateDto, EmulateUpdateDto, EmulateCreateDto, Guid>
{
    private readonly IEmulateService _emulateService;
    public EmulatesController(IEmulateService emulateService) : base(emulateService)
    {
        _emulateService = emulateService;
    }

    /// <summary>
    /// Kiểm tra mã danh hiệu có trong hệ thống không
    /// </summary>
    /// <param name="code"></param>
    /// <return></return>
    /// CreateBy: ddVang (21/8/2023)
    [HttpGet]
    [Route(template: "Exsist-Code")]

    public async Task<bool> CheckExistCode(string code)
    {
        var result = await _emulateService.CheckDuplicateEmulateCodeAsync(code);
        return result;
    }

    /// <summary>
    /// Sinh mã danh hiệu
    /// </summary>
    /// <return></return>
    /// CreateBy: ddVang (21/8/2023)
    [HttpGet]
    [Route(template: "Code")]
    public async Task<string> getCode()
    {
        var result = await _emulateService.getEmulateCode();
        return result;
    }

    /// <summary>
    ///  Lấy danh sách danh hiệu thi đua
    /// </summary>
    /// <return></return>
    /// CreateBy: ddVang (21/8/2023)
    [HttpGet]
    public override async Task<IActionResult> GetListAsync()
    {
        var result = await _emulateService.GetListEmulateDetail();
        return StatusCode(StatusCodes.Status200OK, value: result);
    }

    /// <summary>
    /// Lấy danh sách danh hiệu thi đua theo phân trang
    /// </summary>
    /// <param name="PagingInfoDto"></param>
    /// <return></return>
    /// CreateBy: ddVang (21/8/2023)
    [HttpGet("Filter")]
    public override async Task<IActionResult> GetPageDataAsync([FromQuery] PagingInfoDto pagingInfo)
    {
        var (result, pageIndex, pageSize, totalItems) = await _emulateService.GetPageEmulateAsync(pagingInfo);
        var totalPages = 0;
        if (totalItems != 0 && pageSize != 0)
        {
            totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        }
        var response = new
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems,
            Data = result
        };
        return Ok(response);
    }

    /// <summary>
    /// Thay đổi trạng thái danh hiệu thi đua
    /// </summary>
    /// <param name="changeStatusInfo"></param>
    /// <return></return>
    /// CreateBy: ddVang (10/9/2023)
    [HttpPut]
    [Route(template: "Change-Status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusInfo changeStatusInfo)
    {
        await _emulateService.ChangeStatus(changeStatusInfo.Id, changeStatusInfo.Status);
        return StatusCode(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Thay đổi nhiều trạng thái danh hiệu thi đua
    /// </summary>
    /// <param name="changeStatusInfo"></param>
    /// <return></return>
    /// CreateBy: ddVang (10/9/2023)
    [HttpPut]
    [Route(template: "Change-Multiple-Status")]
    public async Task<IActionResult> ChangeMultipleStatus([FromBody] ChangeListStatusInfoDto changeStatusInfo)
    {
        await _emulateService.ChangeMultipleStatus(changeStatusInfo.Ids, changeStatusInfo.Status);
        return StatusCode(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Xuất file excel
    /// </summary>
    /// <param name="pagingInfo"></param>
    /// <return></return>
    /// CreateBy: ddVang (10/9/2023)
    [HttpGet("Export-Excel")]
    public async Task<IActionResult> ExportExcel([FromQuery] PagingInfoDto pagingInfo)
    {
        var emulateData = await GetEmulateData(pagingInfo);
        using (XLWorkbook wb = new XLWorkbook())
        {
            if(emulateData != null)
            {

                var ws = wb.Worksheets.Add(emulateData);
                ws.Columns().AdjustToContents();
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhHieuThiDua.xlsx");
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseException()
            {
                ErrorCode = ErrorCode.UnknowError
            });
        }
    }

    /// <summary>
    /// Tạo dữ liệu cho file excel
    /// </summary>
    /// <param name="pagingInfo"></param>
    /// <return></return>
    /// CreateBy: ddVang (10/9/2023)
    [NonAction]
    private async Task<DataTable> GetEmulateData([FromQuery] PagingInfoDto pagingInfo)
    {
        DataTable dt = new DataTable();
        dt.TableName = EmulateTable.TableName;
        dt.Columns.Add(EmulateTable.EmulateName, typeof(string));
        dt.Columns.Add(EmulateTable.EmulateCode, typeof(string));
        dt.Columns.Add(EmulateTable.RewardObj, typeof(string));
        dt.Columns.Add(EmulateTable.Rewarder, typeof(string));
        dt.Columns.Add(EmulateTable.RewardType, typeof(string));
        dt.Columns.Add(EmulateTable.EmulateStatus, typeof(string));
        var (result, pageIndex, pageSize, totalItems) = await _emulateService.GetPageEmulateAsync(pagingInfo);
        if(result != null)
        {
            result.ToList().ForEach(item =>
            {
                var status = "";
                if(item.Status == EmulateStatus.Active)
                {
                    status = "Đang sử dụng";
                }else if(item.Status == EmulateStatus.InActive)
                {
                    status = "Ngừng sử dụng";
                }
                dt.Rows.Add(item.EmulateName, item.EmulateCode, item.RewarderName, item.RewardObj, item.RewardType, status);
            });
        }
        return dt;   
        
    }

    /// <summary>
    /// Import file excel
    /// </summary>
    /// <param name="request"></param>
    /// <return></return>
    /// CreateBy: ddVang (10/9/2023)
    [HttpPost("Upload-Excel")]
    public async Task<IActionResult> UploadExcelFile([FromForm] UploadExelFileRequest request)
    {
        UploadExelFileResponse response = new UploadExelFileResponse();
        var path = "../MISA.WebFresher06.CeGov.Domain/UploadFile/" + request.File.FileName;
        try
        {

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                await request.File.CopyToAsync(stream);
            }
            response = await _emulateService.UploadExcelFile(request, path);
            
        }catch(Exception ex)
        {
            response.ISuccess = false;
            response.Message = ex.Message;
        }
        return Ok(response);

    }
}
