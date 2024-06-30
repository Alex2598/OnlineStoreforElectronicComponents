using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreforElectronicComponents.Controllers;
public class ReportsController : Controller
{
    private readonly IReportRepository _reportRepository;
    public ReportsController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    
    public async Task<ActionResult> TopFiveSellingComponents(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
         
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime endDate = eDate ?? DateTime.UtcNow;
            var topFiveSellingComponents = await _reportRepository.GetTopNSellingComponentsByDate(startDate, endDate);
            var vm = new TopNSoldComponentsVm(startDate, endDate, topFiveSellingComponents);
            return View(vm);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Something went wrong";
            return RedirectToAction("Index", "Home");
        }
    }
}