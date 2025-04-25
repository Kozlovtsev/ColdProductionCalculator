namespace ColdProductionCalculator.Services;

public static class CalculationService
{
    private const double Tn = 37;
    private const double Cm = 3800;
    private const double Rho = 1027;

    public static double CalculateQt(double mass, double Tk)
    {
        return (mass * (Tn - Tk) * Cm * Rho) / 3600;
    }

    public static double CalculateQp(double Qt, double Qloss)
    {
        return (Qt + Qloss) * 0.2;
    }

    public static double CalculateEnergy(double powerKw, double hours)
    {
        return powerKw * hours;
    }

    public static List<(double Temperature, double Qt)> GenerateQtGraph(double mass)
    {
        var result = new List<(double, double)>();
        for (double temp = 2; temp <= 10; temp++)
        {
            double qt = CalculateQt(mass, temp);
            result.Add((temp, qt));
        }
        return result;
    }
}
