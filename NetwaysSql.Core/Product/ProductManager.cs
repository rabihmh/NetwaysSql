using Dapper;
using Microsoft.AspNetCore.Mvc;
using Netways.Dynamics.Common.Core;
using Netways.Dynamics.Common.Model;
using Netways.Sql.Core;
using NetwaysSql.Model;
using System.Data;

namespace NetwaysSql.Core
{
    public class ProductManager(ILogger logger, ISqlService<ReadDbContext> readService,ISqlService<WriteDbContext> writeService,IDapperService dapperService) : IProductManager
    {
        public async Task<DefaultResponse<bool>> AddProduct(AddProductDto addProductDto)
        {
            try
            {
                var product = new Product
                {
                    Name = addProductDto.Name,

                    Description = addProductDto.Description ?? "",

                    Price = addProductDto.Price,

                    CategoryId = addProductDto.CategoryId
                };

                var result = await writeService.AddEntity<Product>(product);

                if(result.IsSuccess)
                {
                    return true;
                }

                return DefaultResponseBuilder<bool>.Failure(result.ErrorMessageEn, result.Code, result.IsSystemError).Build();                   

            }
            catch (Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [addProductDto]);
            }
        }

        public async Task<DefaultResponse<int>> AddRangeProduct(IEnumerable<AddProductDto> addProductDtos)
        {
             try
             {
                var products = addProductDtos.Select(x => new Product
                {
                    Name = x.Name,

                    Description = x.Description ?? "",

                    Price = x.Price,

                    CategoryId = x.CategoryId
                });

                var result = await writeService.AddRange<Product>(products);

                if(result.IsSuccess)
                {
                    return DefaultResponse<int>.Success(result.Result);
                }

                return (result.ErrorMessageEn,(int)ErrorCodes.ValidationFailed);

             }catch(Exception ex)
             {
                    logger.LogError(ex, this, [addProductDtos]);
                    return ex;
             }
        }

        public async Task<DefaultResponse<bool>> DeleteProduct(Guid productId)
        {
            try
            {
                var result = await writeService.DeleteEntityById<Product>(productId);

                if(result.IsSuccess)
                {
                    return new DefaultResponse<bool>
                    {
                        Result = true
                    };
                }

                return new DefaultResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    ErrorMessageEn = result.ErrorMessageEn
                };

            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [productId]);
            }
        }

        public async Task<DefaultResponse<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                 var products = await readService.FindAll<Product, ProductDto>(
                     x => true, 
                     x => new ProductDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Price = x.Price,
                         CategoryId = x.CategoryId
                     }
                 );                
                if(products.IsSuccess && products.Result != null)
                {
                   
                    return new DefaultResponse<IEnumerable<ProductDto>>
                    {
                        Result = products.Result,
                    };
                }
                return new DefaultResponse<IEnumerable<ProductDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<ProductDto>>(ex, this,[]);
            }
        }

        public async Task<DefaultResponse<IEnumerable<ProductDto>>> GetAllOrderByName()
        {
            try
            {
                 var products = await readService.FindAll<Product, ProductDto>(
                     x => true, 
                     x => new ProductDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Price = x.Price,
                         CategoryId = x.CategoryId
                     },
                     orderBy: x => x.Name
                 );                
                if(products.IsSuccess && products.Result != null)
                {
                   
                    return new DefaultResponse<IEnumerable<ProductDto>>
                    {
                        Result = products.Result,
                    };
                }
                return new DefaultResponse<IEnumerable<ProductDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<ProductDto>>(ex, this,[]);
            }
        }

        public async Task<DefaultResponse<DefaultPageCollection<ProductDto>>> GetAsPaginated(int pageNumber, int pageSize)
        {
            try
            {
                 var products = await readService.FindAllPaginated<Product, ProductDto>(
                     x => true, 
                     x => new ProductDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Price = x.Price,
                         CategoryId = x.CategoryId
                     },
                     pageNumber,
                     pageSize
                 );                
                if(products.IsSuccess && products.Result != null)
                {
                   
                    return new DefaultResponse<DefaultPageCollection<ProductDto>>
                    {
                        Result = products.Result,
                    };
                }
                return new DefaultResponse<DefaultPageCollection<ProductDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<DefaultPageCollection<ProductDto>>(ex, this,[pageNumber,pageSize]);
            }
        }

        public async Task<DefaultResponse<DefaultPageCollection<ProductDto>>> GetAsPaginatedOrderByName(int pageNumber, int pageSize)
        {
            try
            {
                 var products = await readService.FindAllPaginated<Product, ProductDto>(
                     x => true, 
                     x => new ProductDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         Price = x.Price,
                         CategoryId = x.CategoryId
                     },
                     pageNumber,
                     pageSize,
                     orderBy:x=>x.Name
                 );                
                if(products.IsSuccess && products.Result != null)
                {
                   
                    return new DefaultResponse<DefaultPageCollection<ProductDto>>
                    {
                        Result = products.Result,
                    };
                }
                return new DefaultResponse<DefaultPageCollection<ProductDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<DefaultPageCollection<ProductDto>>(ex, this,[pageNumber,pageSize]);
            }

        }

        public async Task<DefaultResponse<ProductDto>> GetByID(Guid productId)
        {
            try
            {
                var category =await readService.GetEntityById<Product>(productId);

                if(category.IsSuccess && category.Result != null)
                {
                    return new DefaultResponse<ProductDto>
                    {
                        Result = new ProductDto
                        {
                            Id = category.Result.Id,
                            
                            Name = category.Result.Name,
                            
                            Description = category.Result.Description,

                            Price = category.Result.Price,

                            CategoryId = category.Result.CategoryId
                        }
                    };
                }

                return new DefaultResponse<ProductDto>
                {
                    IsSuccess = false,

                    Result = null,

                    ErrorMessageEn=category.ErrorMessageEn,
                };

            }
            catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<ProductDto>(ex, this, [productId]);
            }
        }

        public async Task<DefaultResponse<bool>> UpdateProduct(UpdateProductDto updateProductDto)
        {
            try
            {
                var product = new Product
                {
                    Id = updateProductDto.Id,

                    Name = updateProductDto.Name ??"",

                    Description = updateProductDto.Description ?? "",

                    Price = updateProductDto.Price ?? 0,

                    CategoryId = updateProductDto.CategoryId ?? Guid.Empty
                };

                var result = await writeService.UpdateEntity<Product>(product,updateProductDto.Id);

                if(result.IsSuccess)
                {
                    return new DefaultResponse<bool>
                    {
                        Result = true
                    };
                }

                return new DefaultResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    ErrorMessageEn = result.ErrorMessageEn
                };


            }
            catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex,this,[updateProductDto]);
            }

        }

        public async Task<DefaultResponse<IEnumerable<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            try
            {
                var result=await readService.FindAll<Product>(x => true, ["Category"]);

                if(result.IsSuccess && result.Result != null)
                {
                    var products = result.Result.Select(x => new ProductWithCategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Category= new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name,
                            Description=x.Category.Description
                        }
                    });

                    return new DefaultResponse<IEnumerable<ProductWithCategoryDto>>
                    {
                        Result = products
                    };
                }

                return new DefaultResponse<IEnumerable<ProductWithCategoryDto>>
                {
                    IsSuccess = true,
                    Result = [],
                    ErrorMessageEn = result.ErrorMessageEn
                };

            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<ProductWithCategoryDto>>(ex, this, []);
            }
        }

        public async Task<DefaultResponse<IEnumerable<ProductDto>>> SearchProducts(string keyword)
        {
            try
            {
                var products = await readService.FindAll<Product, ProductDto>(
                    p => p.Name.Contains(keyword) || p.Description.Contains(keyword),
                    p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        CategoryId = p.CategoryId
                    });

                return products.IsSuccess && products.Result != null
                    ? DefaultResponse<IEnumerable<ProductDto>>.Success(products?.Result)
                    : DefaultResponse<IEnumerable<ProductDto>>.Failure(products.ErrorMessageEn,products.Code, products.IsSystemError);

            }
            catch (Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<ProductDto>>(ex, this, [keyword]);
            }
        }

        public async Task<DefaultResponse<IEnumerable<ProductDto>>> GetProductsByCategory(Guid categoryId)
        {
            var result = await readService.FindAll<Product, ProductDto>(
                x => x.CategoryId == categoryId,
                x => new ProductDto { Id = x.Id, Name = x.Name, Price = x.Price, CategoryId = x.CategoryId }
            );

            return result.IsSuccess 
                ? new DefaultResponse<IEnumerable<ProductDto>> { Result = result.Result } 
                : new DefaultResponse<IEnumerable<ProductDto>> { ErrorMessageEn = result.ErrorMessageEn };
        }

        public async Task<DefaultResponse<bool>> UpdateProductCategory(Guid productId, Guid newCategoryId)
        {
            var productResult = await readService.GetEntityById<Product>(productId);
            if (!productResult.IsSuccess || productResult.Result == null)
                return new DefaultResponse<bool> { IsSuccess = false, ErrorMessageEn = "Product not found" };

            productResult.Result.CategoryId = newCategoryId;
            var updateResult = await writeService.UpdateEntity(productResult.Result, productId);

            return updateResult.IsSuccess 
                ? new DefaultResponse<bool> { Result = true } 
                : new DefaultResponse<bool> { IsSuccess = false, ErrorMessageEn = updateResult.ErrorMessageEn };
        }

        public async Task<DefaultResponse<IEnumerable<ProductDto>>> FilterProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var result = await readService.FindAll<Product, ProductDto>(
                x => x.Price >= minPrice && x.Price <= maxPrice,
                x => new ProductDto { Id = x.Id, Name = x.Name, Price = x.Price, CategoryId = x.CategoryId }
            );

            return result.IsSuccess 
                ? new DefaultResponse<IEnumerable<ProductDto>> { Result = result.Result } 
                : new DefaultResponse<IEnumerable<ProductDto>> { IsSuccess = false, ErrorMessageEn = result.ErrorMessageEn };
        }

        public async Task<DefaultResponse<bool>> AddTagToProductAsync(Guid productId, Guid tagId)
        {
            try
            {
                var product = await readService.GetEntityById<Product>(productId);
                if (!product.IsSuccess || product.Result == null)
                    return new DefaultResponse<bool> { IsSuccess = false, ErrorMessageEn = "Product not found" };

                var tag = await readService.GetEntityById<Tag>(tagId);
                if (!tag.IsSuccess || tag.Result == null)
                    return new DefaultResponse<bool> { IsSuccess = false, ErrorMessageEn = "Tag not found" };

                // Check if the association already exists
                var productTagExists = await readService.FindAll<ProductTag>(pt => pt.ProductId == productId && pt.TagId == tagId);

                 if (productTagExists.Result != null && productTagExists.Result.Any())
                    throw new Exception("Product already has this tag.");

                // Add the association to the pivot table
                var productTag = new ProductTag
                {
                    ProductId = productId,
                    TagId = tagId
                };

                var addResult = await writeService.AddEntity(productTag);

                return addResult.IsSuccess
                ? new DefaultResponse<bool> { Result = true, IsSuccess = true }
                : new DefaultResponse<bool> { IsSuccess = false, ErrorMessageEn = addResult.ErrorMessageEn };
            }
            catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [productId, tagId]); 
            }
        }

        public async Task<DefaultResponse<bool>> RemoveTagFromProductAsync(Guid productId, Guid tagId)
        {
            try
            {
                var productTag = await readService.FindAll<ProductTag>(pt=>pt.ProductId == productId &&  pt.TagId== tagId);

                if(!productTag.IsSuccess || productTag.Result==null)
                    return DefaultResponseBuilder<bool>.New().WithMappedResult(productTag).Build();

                var parameters = new DynamicParameters();

                parameters.Add("@ProductId", productId);

                parameters.Add("@TagId", tagId);

                var deleteResult = await dapperService.Delete("DELETE FROM ProductTags WHERE ProductId = @ProductId And TagId = @TagId",parameters);

                if(deleteResult.IsSuccess)
                    return true;

                return DefaultResponseBuilder<bool>.New().WithMappedResult(deleteResult).Build();

            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [productId,tagId]);
            }
        }

        public async Task<DefaultResponse<FullProductDto>> GetProductWithTagsAsync(Guid productId)
        {
            try
            {
                var result = await readService.FindAll<Product>(x => x.Id == productId, new[] { "ProductTags.Tag", "Category" });

                if (!result.IsSuccess || result.Result == null || !result.Result.Any())
                    return new DefaultResponse<FullProductDto> { IsSuccess = false, ErrorMessageEn = "Product not found" };

                var product = result.Result.Select(x => new FullProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = x.Category != null ? new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                        Description = x.Category.Description
                    } : null,
                    Tags = x.ProductTags?.Select(pt => new TagDto
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name
                    }).ToList()
                }).FirstOrDefault();

                return new DefaultResponse<FullProductDto> { Result = product, IsSuccess = product != null };
            }
            catch (Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<FullProductDto>(ex, this, new object[] { productId });
            }
        }

        public async Task<DefaultResponse<FullProductDto>> GetProductWithTagsDapper(Guid productId)
        {
            try
            {
                const string query = @"
                SELECT 
                    p.Id, p.Name, p.Description, p.Price, p.CategoryId,
                    c.Id AS CategoryId, c.Name AS CategoryName, c.Description AS CategoryDescription,
                    t.Id AS TagId, t.Name AS TagName
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId = c.Id
                LEFT JOIN ProductTags pt ON p.Id = pt.ProductId
                LEFT JOIN Tags t ON pt.TagId = t.Id
                WHERE p.Id = @ProductId";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", productId);

                var result = await dapperService.GetRelatedEntities<Product, Category, Tag>(
                    query,
                    (product, category, tag) =>
                    {
                        product.Category = category;
                        if (tag != null)
                        {
                            product.ProductTags ??= new List<ProductTag>();
                            product.ProductTags.Add(new ProductTag { Tag = tag });
                        }
                        return product;
                    },
                    parameters,
                    splitOn: "CategoryId,TagId",
                    commandType: CommandType.Text);

                if (!result.IsSuccess || result.Result == null)
                    return DefaultResponseBuilder<FullProductDto>.New().WithMappedResult(result).Build();

                var product = result.Result.Select(x => new FullProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = x.Category != null ? new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                        Description = x.Category.Description
                    } : null,
                    Tags = x.ProductTags?.Select(pt => new TagDto
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name
                    }).ToList()
                }).FirstOrDefault();

                return product;
            }
            catch (Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<FullProductDto>(ex, this, new object[] { productId });
            }
        }
    }
}
