using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public interface ICrudService<TEntityDto,TEntiTyUpdateDto, TEntityCreateDto, TKey> : IReadOnlyService<TEntityDto, TKey> where TEntityDto : class where TEntiTyUpdateDto : class where TEntityCreateDto : class
    {
        /// <summary>
        /// Hàm thêm một bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> AddAsync(TEntityCreateDto entity);

        /// <summary>
        /// Hàm chỉnh sửa một bản ghi
        /// </summary>
        /// <returns>/returns>
        /// CreateBy: ddVang (21/8/2023)
        Task<int> UpdateAsync(TKey id, TEntiTyUpdateDto entity);

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
