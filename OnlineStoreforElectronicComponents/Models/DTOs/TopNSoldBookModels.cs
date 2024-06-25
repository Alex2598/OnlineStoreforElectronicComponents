namespace OnlineStoreforElectronicComponents.Models.DTOs;

public record TopNSoldComponentModel(string ComponentName, string Package, int TotalUnitSold);
public record TopNSoldComponentsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldComponentModel> TopNSoldComponents);