using Microsoft.EntityFrameworkCore;

namespace OnlineStoreforElectronicComponents.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByComponentId(int componentId) => await _context.Stocks.FirstOrDefaultAsync(s => s.ComponentId == componentId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given component id, then add new record
            // if there is already stock for given component id, update stock's quantity
            var existingStock = await GetStockByComponentId(stockToManage.ComponentId);
            if (existingStock is null)
            {
                var stock = new Stock { ComponentId = stockToManage.ComponentId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from component in _context.Components
                                join stock in _context.Stocks
                                on component.Id equals stock.ComponentId
                                into component_stock
                                from componentStock in component_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || component.ComponentName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    ComponentId = component.Id,
                                    ComponentName = component.ComponentName,
                                    Quantity = componentStock == null ? 0 : componentStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByComponentId(int componentId);
        Task ManageStock(StockDTO stockToManage);
    }
}
