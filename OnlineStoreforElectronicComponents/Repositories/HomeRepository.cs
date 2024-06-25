

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

        public async Task<IEnumerable<TypeOfComponent>> TypeOfComponents()
        {
            return await _db.TypeOfComponents.ToListAsync();
        }
        public async Task<IEnumerable<Component>> GetComponents(string sTerm = "", int typeOfComponentId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Component> components = await (from component in _db.Components
                         join typeofcomponent in _db.TypeOfComponents
                         on component.TypeOfComponentId equals typeofcomponent.Id
                         join stock in _db.Stocks
                         on component.Id equals stock.ComponentId
                         into component_stocks
                         from componentWithStock in component_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (component != null && component.ComponentName.ToLower().StartsWith(sTerm))
                         select new Component
                         {
                             Id = component.Id,
                             Image = component.Image,
                             Package = component.Package,
                             ComponentName = component.ComponentName,
                             TypeOfComponentId = component.TypeOfComponentId,
                             Price = component.Price,
                             TypeOfComponentName = typeofcomponent.TypeOfComponentName,
                             Quantity=componentWithStock==null? 0:componentWithStock.Quantity
                         }
                         ).ToListAsync();
            if (typeOfComponentId > 0)
            {

                components = components.Where(a => a.TypeOfComponentId == typeOfComponentId).ToList();
            }
            return components;

        }
    }
}
