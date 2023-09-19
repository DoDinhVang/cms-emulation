using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public interface IEmulateService : ICrudService<EmulateDto, EmulateUpdateDto, EmulateCreateDto, Guid>
    {
        /// <summary>
        /// Hàm kiểm tra xem mã danh hiệu đã có trong hệ thông hay không
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<bool> CheckDuplicateEmulateCodeAsync(string code);

        /// <summary>
        /// Hàm lấy danh sách chi tiết danh hiệu thi đua
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<List<EmulateDetailDto>> GetListEmulateDetail();

        /// <summary>
        /// Hàm sinh mã danh hiệu
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<String> getEmulateCode();

        /// <summary>
        /// Hàm lấy danh sách danh hiệu thi đua theo phân trang
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<(IEnumerable<EmulateDetailDto>, int, int, int)> GetPageEmulateAsync(PagingInfoDto pagingInfo);

        /// <summary>
        /// Hàm cập nhật trạng thái danh hiệu thi đua
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> ChangeStatus(Guid id, EmulateStatus status);

        /// <summary>
        /// Hàm cập nhật nhiều trạng thái danh hiệu thi đua
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> ChangeMultipleStatus(List<Guid> ids, EmulateStatus status);

        /// <summary>
        /// Hàm Import file Excel và lưu file vào folder ExcelFileFolder và lấy giá trị đẩy   vào database
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (10/9/2023)
        public Task<UploadExelFileResponse> UploadExcelFile(UploadExelFileRequest request, string path);
    }
}
