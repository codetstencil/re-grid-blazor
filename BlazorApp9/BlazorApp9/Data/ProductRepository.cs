using BlazorApp9.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp9.Data
{
    public class ProductRepository : SqlRepository<Product>
    {
        public ProductRepository(NorthwindDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Product> GetAll()
        {
            return EfDbSet;
        }

        public override async Task<Product> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(c => c.ProductID == (int)id);
        }
    }
}