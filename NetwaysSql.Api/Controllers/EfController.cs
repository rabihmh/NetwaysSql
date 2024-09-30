using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetwaysSql.Core;
using NetwaysSql.Model;

namespace NetwaysSql.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class EfController(ICategoryManager categoryManager) : ControllerBase
    {
        [HttpGet]
        [Route("categories/{categoryId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            var response = await categoryManager.GetByID(categoryId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost]
        [Route("categories")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryDto addCategoryDto)
        {
            var response = await categoryManager.AddCategory(addCategoryDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("categories")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await categoryManager.GetAll();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("categories/{categoryId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var response = await categoryManager.DeleteCategory(categoryId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        [Route("categories")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryDto updateCategoryDto)
        {
            var response = await categoryManager.UpdateCategory(updateCategoryDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("categories/{pageNumber}/{pageSize}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetCategoriesAsPaginated(int pageNumber, int pageSize)
        {
            var response = await categoryManager.GetAsPaginated(pageNumber, pageSize);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("categories/orderByName/{pageNumber}/{pageSize}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetCategoriesAsPaginatedOrderByName(int pageNumber, int pageSize)
        {
            var response = await categoryManager.GetAsPaginatedOrderByName(pageNumber, pageSize);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [Route("categories/range")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> AddRangeCategory([FromBody]IEnumerable<AddCategoryDto> addCategoryDtos)
        {
            var response = await categoryManager.AddRangeCategory(addCategoryDtos);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
