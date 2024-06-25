using Microsoft.EntityFrameworkCore;

namespace OnlineStoreforElectronicComponents.Repositories;

public interface ITypeOfComponentRepository
{
    Task AddTypeOfComponent(TypeOfComponent typeofcomponent);
    Task UpdateTypeOfComponent(TypeOfComponent typeofcomponent);
    Task<TypeOfComponent?> GetTypeOfComponentById(int id);
    Task DeleteTypeOfComponent(TypeOfComponent typeofcomponent);
    Task<IEnumerable<TypeOfComponent>> GetTypeOfComponents();
}
public class TypeOfComponentRepository : ITypeOfComponentRepository
{
    private readonly ApplicationDbContext _context;
    public TypeOfComponentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTypeOfComponent(TypeOfComponent typeofcomponent)
    {
        _context.TypeOfComponents.Add(typeofcomponent);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateTypeOfComponent(TypeOfComponent typeofcomponent)
    {
        _context.TypeOfComponents.Update(typeofcomponent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTypeOfComponent(TypeOfComponent typeofcomponent)
    {
        _context.TypeOfComponents.Remove(typeofcomponent);
        await _context.SaveChangesAsync();
    }

    public async Task<TypeOfComponent?> GetTypeOfComponentById(int id)
    {
        return await _context.TypeOfComponents.FindAsync(id);
    }

    public async Task<IEnumerable<TypeOfComponent>> GetTypeOfComponents()
    {
        return await _context.TypeOfComponents.ToListAsync();
    }

    
}
