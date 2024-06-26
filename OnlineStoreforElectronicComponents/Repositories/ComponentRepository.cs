using Microsoft.EntityFrameworkCore;

namespace OnlineStoreforElectronicComponents.Repositories
{
    public interface IComponentRepository
    {
        Task AddComponent(Component component);
        Task DeleteComponent(Component component);
        Task<Component?> GetComponentById(int id);
        Task<IEnumerable<Component>> GetComponents();
        Task UpdateComponent(Component component);
    }

    public class ComponentRepository : IComponentRepository
    {
        private readonly ApplicationDbContext _context;
        public ComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddComponent(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComponent(Component component)
        {
            _context.Components.Update(component);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComponent(Component component)
        {
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
        }

        public async Task<Component?> GetComponentById(int id) => await _context.Components.FindAsync(id);

        public async Task<IEnumerable<Component>> GetComponents() => await _context.Components.Include(a=>a.Category).ToListAsync();
    }
}
