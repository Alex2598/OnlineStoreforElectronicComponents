using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreforElectronicComponents.Repositories;

[Authorize(Roles = nameof(Roles.Admin))]
public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;
    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TopNSoldComponentModel>> GetTopNSellingComponentsByDate(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        var topFiveSoldComponents = await _context.Database.SqlQueryRaw<TopNSoldComponentModel>("exec Usp_GetTopNSellingComponentsByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return topFiveSoldComponents;
    }

}

public interface IReportRepository
{
    Task<IEnumerable<TopNSoldComponentModel>> GetTopNSellingComponentsByDate(DateTime startDate, DateTime endDate);
}