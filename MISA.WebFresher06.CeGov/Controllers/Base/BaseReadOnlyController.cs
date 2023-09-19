using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Application.Dto;

namespace MISA.WebFresher06.CeGov.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseReadOnlyController<TEntityDto, TKey> : Controller where TEntityDto : class
    {
        public readonly IReadOnlyService<TEntityDto, TKey> ReadOnlyService;
        public BaseReadOnlyController(IReadOnlyService<TEntityDto, TKey> readOnlyService)
        {
            ReadOnlyService = readOnlyService;
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [HttpGet]
        public virtual async Task<IActionResult> GetListAsync()
        {
            var result = await ReadOnlyService.GetListAsync();

            return StatusCode(StatusCodes.Status200OK, value: result);
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [Route(template: "{id}")]
        [HttpGet]
        public virtual async Task<IActionResult> GetAsync(TKey id)
        {
            var result = await ReadOnlyService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, value: result);
        }

        /// <summary>
        /// Hàm lấy danh bản ghi theo pagingInfo: {pageIndex, pageSize}
        /// </summary>
        /// <param name="paginInfo"></param>
        /// <return></return>
        /// CreateBy: ddVang (21/8/2023)
        [HttpGet("Filter")]
        public virtual async Task<IActionResult> GetPageDataAsync([FromQuery]PagingInfoDto pagingInfo)
        {
            var (result, pageIndex, pageSize, totalItems) = await ReadOnlyService.GetPageDataAsync(pagingInfo);
            var totalPages = 0;
            if(totalItems != 0 && pageSize != 0)
            {
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            }
          
            if (result == null)
            {
                return NotFound();
            }
            var response = new
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Data =  result
            };
            return Ok(response);
        }
    }
}
