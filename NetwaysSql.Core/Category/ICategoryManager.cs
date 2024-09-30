namespace NetwaysSql.Core
{
    using Netways.Dynamics.Common.Model;
    using NetwaysSql.Model;

    public interface ICategoryManager
    {
        Task<DefaultResponse<bool>> AddCategory(AddCategoryDto addCategoryDto);

        Task<DefaultResponse<int>> AddRangeCategory(IEnumerable<AddCategoryDto> addCategoryDtos);

        Task<DefaultResponse<bool>> DeleteCategory(Guid categoryId);

        Task<DefaultResponse<bool>> UpdateCategory(UpdateCategoryDto updateCategoryDto);

        Task<DefaultResponse<CategoryDto>> GetByID(Guid categoryId);

        Task<DefaultResponse<IEnumerable<CategoryDto>>> GetAll();

        Task<DefaultResponse<IEnumerable<CategoryDto>>> GetAllOrderByName();

        Task<DefaultResponse<DefaultPageCollection<CategoryDto>>> GetAsPaginated(int pageNumber, int pageSize);

        Task<DefaultResponse<DefaultPageCollection<CategoryDto>>> GetAsPaginatedOrderByName(int pageNumber, int pageSize);
    }
}
