using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;

namespace MISA.WebFresher06.CeGov.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseCrudController<TEntityDto, TEntiTyUpdateDto, TEntityCreateDto, TKey> : BaseReadOnlyController<TEntityDto, TKey> where TEntityDto: class where TEntiTyUpdateDto : class where TEntityCreateDto : class
    {
        public readonly ICrudService<TEntityDto, TEntiTyUpdateDto, TEntityCreateDto, TKey> CrudService;
        public BaseCrudController(ICrudService<TEntityDto, TEntiTyUpdateDto, TEntityCreateDto, TKey> crudService) : base (crudService)
        {
            CrudService = crudService;
        }

        /// <summary>
        /// Hàm  Xóa một bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [Route("{id}")]
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(TKey id)
        {
            await CrudService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);

        }
        /// <summary>
        /// Hàm  Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteManyAsync(List<TKey> ids)
        {
           await CrudService.DeleteManyAsync(ids);
           return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="emulate"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [HttpPost]
        public virtual async Task<IActionResult> AddAsync(TEntityCreateDto entityCreadDto)
        {
           await CrudService.AddAsync(entityCreadDto);
           return StatusCode(StatusCodes.Status201Created);

        }

        /// <summary>
        /// Chỉnh sửa một bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedEmulate"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [Route("{id}")]
        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(TKey id, TEntiTyUpdateDto entityUpdateDto)
        {
            await CrudService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
