using practices.Model;
using practices.Repositories;
namespace practices.CQRS.Queries
{
    public class QueryHandler
    {
        private readonly IProductRepository _productRepository;
        public QueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> HandleGetall()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<Product> HandleGetById(GetProductByIdQuery query)
        {
            return await _productRepository.GetProductByIdAsync(query.Id);
        }
    }
}
