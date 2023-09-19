using MISA.WebFresher06.CeGov.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public interface IEmulateRepository : ICrudRepository<Emulate, Guid>
    {
        /// <summary>
        /// Hàm tìm kiếm emulateCode
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<Emulate?> FindByEmulateCodeAsync(string code);

        /// <summary>
        /// Hàm lấy thông tin chi tiết danh sách danh hiệu thi đua
        /// </summary>
        /// <returns>Danh sách danh hiệu thi đua</returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<List<EmulateDetail>> GetListEmulate();

        /// <summary>
        /// Hàm lấy thông tin chi tiết danh hiệu thi đua theo phân trang
        /// </summary>
        /// <returns>Danh sách danh hiệu thi đua</returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<(IEnumerable<EmulateDetail>, int, int, int)> GetPageEmulateAsync(int pageIndex, int pageSize, string? keyWord, string? rewardObj, string? rewardType, Guid? rewarderId, int? status);

        /// <summary>
        /// Hàm cập nhật trạng thái danh hiệu thi đua
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (10/8/2023)
        Task<int> ChangeStatus(Guid id, EmulateStatus status);

        /// <summary>
        /// Hàm cập nhật nhiều trạng thái danh hiệu thi đua
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (10/8/2023)
        Task<int> ChangeMultipleStatus(List<Guid> ids, EmulateStatus status);

        /// <summary>
        /// Hàm upload file excel và đẩy dữ liệu vào database
        /// </summary>
        /// <returns></returns>
        /// CreateBy: ddVang (21/8/2023)
        public Task<UploadExelFileResponse> UploadExcelFile(UploadExelFileRequest request, string path);

    }
}
