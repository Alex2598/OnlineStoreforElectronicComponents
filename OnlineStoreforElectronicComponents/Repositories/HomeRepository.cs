

using Microsoft.EntityFrameworkCore;

namespace OnlineStoreforElectronicComponents.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Component>> GetComponents(string sTerm = "", int categoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Component> components = await (from component in _db.Components
                         join category in _db.Categories
                         on component.CategoryId equals category.Id
                         join stock in _db.Stocks
                         on component.Id equals stock.ComponentId
                         into component_stocks
                         from componentWithStock in component_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (component != null && component.ComponentName.ToLower().StartsWith(sTerm))
                         select new Component
                         {
                             Id = component.Id,
                             Image = component.Image,
                             PackageName = component.PackageName,
                             ComponentName = component.ComponentName,
                             CategoryId = component.CategoryId,
                             Price = component.Price,
                             CategoryName = category.CategoryName,
                             Quantity=componentWithStock==null? 0:componentWithStock.Quantity
                         }
                         ).ToListAsync();
            if (categoryId > 0)
            {

                components = components.Where(a => a.CategoryId == categoryId).ToList();
            }
            return components;

        }
    }
}
