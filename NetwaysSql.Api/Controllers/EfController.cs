﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetwaysSql.Core;
using NetwaysSql.Model;

namespace NetwaysSql.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class EfController(ICategoryManager categoryManager,IProductManager productManager) : ControllerBase
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

        //products api

        [HttpGet]
        [Route("products/{productId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var response = await productManager.GetByID(productId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [Route("products")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> AddProduct([FromBody]AddProductDto addProductDto)
        {
            var response = await productManager.AddProduct(addProductDto);

            return response.ToApiResponse();
        }

        [HttpGet]
        [Route("products")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await productManager.GetAll();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("products/{productId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var response = await productManager.DeleteProduct(productId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        [Route("products")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductDto updateProductDto)
        {
            var response = await productManager.UpdateProduct(updateProductDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("products/{pageNumber}/{pageSize}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductsAsPaginated(int pageNumber, int pageSize)
        {
            var response = await productManager.GetAsPaginated(pageNumber, pageSize);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("products/orderByName/{pageNumber}/{pageSize}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductsAsPaginatedOrderByName(int pageNumber, int pageSize)
        {
            var response = await productManager.GetAsPaginatedOrderByName(pageNumber, pageSize);

            return response.ToApiResponse();

        }

        [HttpPost]
        [Route("products/range")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> AddRangeProducts([FromBody]IEnumerable<AddProductDto> addProductDto)
        {
            var response = await productManager.AddRangeProduct(addProductDto);

            return response.ToApiResponse();

        }

        [HttpGet]
        [Route("productswithcategories")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var response = await productManager.GetProductsWithCategory();

            return response.ToApiResponse();

        }

        [HttpGet]
        [Route("products/search/{keyword}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> SearchProducts(string keyword)
        {
            var response = await productManager.SearchProducts(keyword);

            return response.ToApiResponse();

        }

        [HttpGet]
        [Route("products/category/{categoryId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductsByCategory(Guid categoryId)
        {
            var response = await productManager.GetProductsByCategory(categoryId);

            return response.ToApiResponse();

        }

        [HttpPut]
        [Route("products/updatecategory")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> UpdateProductCategory(Guid productId, Guid newCategoryId)
        {
            var response = await productManager.UpdateProductCategory(productId, newCategoryId);

            return response.ToApiResponse();

        }

        [HttpGet]
        [Route("products/filter/{minPrice}/{maxPrice}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> FilterProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var response = await productManager.FilterProductsByPriceRange(minPrice, maxPrice);

            return response.ToApiResponse();
        }

        [HttpPost]
        [Route("products/addtag")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> AddTagToProductAsync(Guid productId, Guid tagId)
        {
            var response = await productManager.AddTagToProductAsync(productId, tagId);

            return response.ToApiResponse();
        }

        [HttpDelete]
        [Route("products/removetag")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> RemoveTagFromProductAsync(Guid productId, Guid tagId)
        {
            var response = await productManager.RemoveTagFromProductAsync(productId, tagId);

            return response.ToApiResponse();
        }

        [HttpGet]
        [Route("products/tags/{productId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductWithTagsAsync(Guid productId)
        {
            var response = await productManager.GetProductWithTagsAsync(productId);

            return response.ToApiResponse();
        }

        [HttpGet]
        [Route("products/dapper/tags/{productId}")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetProductWithTagsDapperAsync(Guid productId)
        {
            var response = await productManager.GetProductWithTagsDapper(productId);

            return response.ToApiResponse();
        }
    }
}
