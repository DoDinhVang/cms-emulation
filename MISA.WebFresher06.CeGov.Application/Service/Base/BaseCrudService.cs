using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public abstract class BaseCrudService<TEntity, TEntityDto, TEntiTyUpdateDto, TEntityCreateDto, TKey> : BaseReadOnlyService<TEntity, TEntityDto, TKey>, ICrudService<TEntityDto, TEntiTyUpdateDto, TEntityCreateDto, TKey> where TEntity : IEntity<TKey> where TEntityDto : class where TEntiTyUpdateDto : class where TEntityCreateDto : class
    {
        protected readonly ICrudRepository<TEntity, TKey> CrudRepository;
        protected BaseCrudService(ICrudRepository<TEntity, TKey> repository) : base(repository)
        {
            CrudRepository = repository;
        }

        public  async Task<int> AddAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapTEntityCreateDtoToTEntity(entityCreateDto);
            var result = await CrudRepository.AddAsync(entity);
            return result;
        }

        public async Task<int> DeleteAsync(TKey id)
        {
            var data  = await GetAsync(id);
            if(data == null)
            {
                throw new BadRequestException($"{ErrorMessage.NonExistingIds}: {id}");
            }

            var result = await CrudRepository.DeleteAsync(id);
            return result;
        }

        public async Task<int> DeleteManyAsync(List<TKey> ids)
        {
            var existingIds = await GetExistingIdsAsync(ids);
            var nonExistingIds = ids.Except(existingIds).ToList();
            if (nonExistingIds.Count != 0)
            {
                throw new BadRequestException($"{ErrorMessage.NonExistingIds}: {string.Join(',', nonExistingIds)}");
            }
            var result = await CrudRepository.DeleteManyAsync(ids);
            return result;
        }

        public async Task<int> UpdateAsync(TKey id, TEntiTyUpdateDto entityUpdateDto)
        {
            var entity = await MapTEntityUpdateDtoToTEntity(id,entityUpdateDto);
            var result = await CrudRepository.UpdateAsync(id, entity);
            return result;
        }

        public abstract Task<TEntity> MapTEntityCreateDtoToTEntity(TEntityCreateDto entity);
        public abstract Task<TEntity>  MapTEntityUpdateDtoToTEntity(TKey id ,TEntiTyUpdateDto entity);
    }
}
