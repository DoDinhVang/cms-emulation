using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public interface IReadOnlyService<TEntityDto, TKey> where TEntityDto : class
    {
        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Trả về danh sách bản ghi</returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<List<TEntityDto>> GetListAsync();

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <returns>Trả về một bản ghi theo id truyền vào tương ứng/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<TEntityDto> GetAsync(TKey id);

        /// <summary>
        /// Hàm lấy bản ghi theo{pageIndex,pageSize}
        /// </summary>
        /// <returns>Trả về danh sách bản ghi tương ứng</returns>
        /// CreateBy: ddVang (24/8/2023)
        Task<(IEnumerable<TEntityDto>, int, int, int)> GetPageDataAsync(PagingInfoDto pagingInfo);
        Task<List<TKey>> GetExistingIdsAsync(List<TKey> ids);

    }
}
