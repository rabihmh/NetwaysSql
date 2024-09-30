using Netways.Dynamics.Common.Core;
using Netways.Dynamics.Common.Model;
using Netways.Sql.Core;
using NetwaysSql.Model;

namespace NetwaysSql.Core
{
    public class CategoryManager(ILogger logger,ISqlService<ReadDbContext> readService,ISqlService<WriteDbContext> writeService) : ICategoryManager
    {
        public async Task<DefaultResponse<bool>> AddCategory(AddCategoryDto addCategoryDto)
        {
            try
            {
                var category = new Category
                {
                    Name = addCategoryDto.Name,
                    Description = addCategoryDto.Description
                };

                var result = await writeService.AddEntity<Category>(category);

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
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [addCategoryDto]);
            }
        }

        public async Task<DefaultResponse<int>> AddRangeCategory(IEnumerable<AddCategoryDto> addCategoryDtos)
        {
            try
            {
                var categories = addCategoryDtos.Select(x => new Category
                {
                    Name = x.Name,
                    Description = x.Description
                });

                var result = await writeService.AddRange<Category>(categories);

                if(result.IsSuccess)
                {
                    return new DefaultResponse<int>
                    {
                        Result = result.Result
                    };
                }

                return new DefaultResponse<int>
                {
                    IsSuccess = false,
                    Result = 0,
                    ErrorMessageEn = result.ErrorMessageEn
                };

            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<int>(ex, this, [addCategoryDtos]);
            }
        }

        public async Task<DefaultResponse<bool>> DeleteCategory(Guid categoryId)
        {
            try
            {
                var result = await writeService.DeleteEntityById<Category>(categoryId);

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
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [categoryId]);
            }
        }

        public async  Task<DefaultResponse<IEnumerable<CategoryDto>>> GetAll()
        {
            try
            {
                 var categories = await readService.FindAll<Category, CategoryDto>(
                     x => true,//means without applying any filter  
                     x => new CategoryDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description
                         //this is how we can map the properties of the entity to the dto(Category to CategoryDto)
                     }
                 );                
                if(categories.IsSuccess && categories.Result != null)
                {
                   
                    return new DefaultResponse<IEnumerable<CategoryDto>>
                    {
                        Result = categories.Result,
                    };
                }
                return new DefaultResponse<IEnumerable<CategoryDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<CategoryDto>>(ex, this,[]);
            }
        }

        public async Task<DefaultResponse<IEnumerable<CategoryDto>>> GetAllOrderByName()
        {
            try
            {
                 var categories = await readService.FindAll<Category, CategoryDto>(
                     x => true,//means without applying any filter  
                     x => new CategoryDto
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description
                     },
                     orderBy:x => x.Name
                 );                
                if(categories.IsSuccess && categories.Result != null)
                {
                   
                    return new DefaultResponse<IEnumerable<CategoryDto>>
                    {
                        Result = categories.Result,
                    };
                }
                return new DefaultResponse<IEnumerable<CategoryDto>>
                { 
                    IsSuccess = false,
                   Result = null
                };
               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<IEnumerable<CategoryDto>>(ex, this,[]);
            }
        }

        public async Task<DefaultResponse<CategoryDto>> GetByID(Guid categoryId)
        {
            try
            {
                var category =await readService.GetEntityById<Category>(categoryId);

                if(category.IsSuccess && category.Result != null)
                {
                    return new DefaultResponse<CategoryDto>
                    {
                        Result = new CategoryDto
                        {
                            Id = category.Result.Id,
                            Name = category.Result.Name,
                            Description = category.Result.Description
                        }
                    };
                }

                return new DefaultResponse<CategoryDto>
                {
                    IsSuccess = false,

                    Result = null,

                    ErrorMessageEn=category.ErrorMessageEn,
                };

            }
            catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<CategoryDto>(ex, this, [categoryId]);
            }
        }

        public async Task<DefaultResponse<bool>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = new Category
                {
                    Id = updateCategoryDto.Id,
                    Name = updateCategoryDto.Name,
                    Description = updateCategoryDto.Description
                };

                var result = await writeService.UpdateEntity<Category>(category,updateCategoryDto.Id);

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
                return logger.LogErrorAndReturnDefaultResponse<bool>(ex, this, [updateCategoryDto]);
            }
        }

        public async Task<DefaultResponse<DefaultPageCollection<CategoryDto>>> GetAsPaginated(int pageNumber, int pageSize)
        {
            try
            {
                var categories=await readService.FindAllPaginated<Category, CategoryDto>(
                    x => true,
                    x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    },
                    pageNumber,
                    pageSize
                );

                if(categories.IsSuccess && categories.Result != null)
                {
                    return new DefaultResponse<DefaultPageCollection<CategoryDto>>
                    {
                        Result = categories.Result
                    };
                }

                return new DefaultResponse<DefaultPageCollection<CategoryDto>>
                {
                    IsSuccess = false,
                    Result = null
                };

               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<DefaultPageCollection<CategoryDto>>(ex, this,[pageNumber,pageSize]);
            }
        }

        public async Task<DefaultResponse<DefaultPageCollection<CategoryDto>>> GetAsPaginatedOrderByName(int pageNumber, int pageSize)
        {
            try
            {
                var categories=await readService.FindAllPaginated<Category, CategoryDto>(
                    x => true,
                    x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    },
                    pageNumber,
                    pageSize,
                    x => x.Name,
                    ascending:true
                );

                if(categories.IsSuccess && categories.Result != null)
                {
                    return new DefaultResponse<DefaultPageCollection<CategoryDto>>
                    {
                        Result = categories.Result
                    };
                }

                return new DefaultResponse<DefaultPageCollection<CategoryDto>>
                {
                    IsSuccess = false,
                    Result = null
                };

               
            }catch(Exception ex)
            {
                return logger.LogErrorAndReturnDefaultResponse<DefaultPageCollection<CategoryDto>>(ex, this,[pageNumber,pageSize]);
            }
        }
    }
}
