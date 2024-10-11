using Netways.Dynamics.Common.Model;
using NetwaysSql.Model;

namespace NetwaysSql.Core
{
    public interface IProductManager
    {
        Task<DefaultResponse<bool>> AddProduct(AddProductDto addProductDto);

        Task<DefaultResponse<int>> AddRangeProduct(IEnumerable<AddProductDto> addProductDtos);

        Task<DefaultResponse<bool>> DeleteProduct(Guid productId);

        Task<DefaultResponse<bool>> UpdateProduct(UpdateProductDto updateProductDto);

        Task<DefaultResponse<ProductDto>> GetByID(Guid productId);

        Task<DefaultResponse<IEnumerable<ProductDto>>> GetAll();

        Task<DefaultResponse<IEnumerable<ProductDto>>> GetAllOrderByName();

        Task<DefaultResponse<DefaultPageCollection<ProductDto>>> GetAsPaginated(int pageNumber, int pageSize);

        Task<DefaultResponse<DefaultPageCollection<ProductDto>>> GetAsPaginatedOrderByName(int pageNumber, int pageSize);
    }
}
