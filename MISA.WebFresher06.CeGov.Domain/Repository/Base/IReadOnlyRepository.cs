using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Trả về danh sách bản ghi</returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <returns>Trả về một bản ghi theo id truyền vào tương ứng/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<TEntity> GetAsync(TKey id);

        /// <summary>
        /// Tìm kiếm một bản ghi theo id
        /// </summary>
        /// <returns>Trả về một bản ghi theo id tương ứng</returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<TEntity?> FindAsync(TKey id);

        /// <summary>
        /// Hàm lấy bản ghi theo{pageIndex,pageSize}
        /// </summary>
        /// <returns>Trả về danh sách bản ghi tương ứng</returns>
        /// CreateBy: ddVang (24/8/2023)
        Task<(IEnumerable<TEntity>, int, int, int)> GetPageDataAsync(int pageIndex, int pageSize, string? keyWord );

        Task<List<TKey>> GetExistingIdsAsync(List<TKey> ids);

    }
}
