using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public abstract class BaseReadOnlyService<TEntity,TEntityDto,TKey> : IReadOnlyService<TEntityDto, TKey> where TEntityDto : class where TEntity : IEntity<TKey>
    {
        protected readonly IReadOnlyRepository<TEntity, TKey> ReadOnlyRepository;
        public BaseReadOnlyService(IReadOnlyRepository<TEntity, TKey> repository)
        {
            ReadOnlyRepository = repository;
        }

        public async Task<TEntityDto> GetAsync(TKey id)
        {
            var entity = await ReadOnlyRepository.GetAsync(id);
            var result = MapTEntityToEntityDto(entity);
            return result;
        }

        public async Task<List<TKey>> GetExistingIdsAsync(List<TKey> ids)
        {
            var result = await ReadOnlyRepository.GetExistingIdsAsync(ids);
            return result;
        }

        public async Task<List<TEntityDto>> GetListAsync()
        {
            var entities = await ReadOnlyRepository.GetListAsync();
            var result = entities.Select(entity => MapTEntityToEntityDto(entity)).ToList();
            return result;
        }

        public async Task<(IEnumerable<TEntityDto>, int, int, int)> GetPageDataAsync(PagingInfoDto pagingInfo)
        {
            var (entities, pageIndex, pageSize, totalItems) = await ReadOnlyRepository.GetPageDataAsync(pagingInfo.PageIndex, pagingInfo.PageSize, pagingInfo.KeyWord);

            var result = entities.Select(entity => MapTEntityToEntityDto(entity)).ToList();
            return (result, pageIndex, pageSize, totalItems);
        }

        public abstract TEntityDto MapTEntityToEntityDto(TEntity entity);

    }
}
