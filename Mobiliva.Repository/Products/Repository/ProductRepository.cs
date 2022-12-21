using Mobiliva.DAL;
using Mobiliva.DAL.Entities;
using Mobiliva.Repository.Base;
using Mobiliva.Repository.Products.Interface;

namespace Mobiliva.Repository.Customers.Repository
{
    public class ProductRepository : BaseRepository<Product>,  IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
