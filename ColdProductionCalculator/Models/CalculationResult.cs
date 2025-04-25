namespace ColdProductionCalculator.Models;

public class CalculationResult
{
    public DateTime Timestamp { get; set; }
    public int CowCount { get; set; }
    public double MilkPerCow { get; set; }
    public double FinalTemp { get; set; }
    public double PowerKw { get; set; }
    public double WorkHours { get; set; }
    public double Qt { get; set; }
    public double Qp { get; set; }
    public double EnergyConsumption { get; set; }
}
