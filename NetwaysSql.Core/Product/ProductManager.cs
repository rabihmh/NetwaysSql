using Netways.Dynamics.Common.Core;
using Netways.Dynamics.Common.Model;
using Netways.Sql.Core;
using NetwaysSql.Model;

namespace NetwaysSql.Core
{
    public class ProductManager(ILogger logger, ISqlService<ReadDbContext> readService,ISqlService<WriteDbContext> writeService) : IProductManager
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

                return result.ErrorMessageEn;

            }catch (Exception ex)
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

    }
}
