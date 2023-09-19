using MISA.WebFresher06.CeGov.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public interface ICrudRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Hàm thêm một bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Hàm chỉnh sửa một bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> UpdateAsync(TKey id, TEntity entity);

        /// <summary>
        /// Hàm xóa một bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> DeleteManyAsync(List<TKey> ids);
    }
}
