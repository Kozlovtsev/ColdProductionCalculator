using Newtonsoft.Json;
using ColdProductionCalculator.Models;
using System.IO;

namespace ColdProductionCalculator.Helpers;

public static class JsonStorage
{
    private static readonly string filePath = "history.json";

    public static void SaveHistory(List<CalculationResult> history)
    {
        var json = JsonConvert.SerializeObject(history, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static List<CalculationResult> LoadHistory()
    {
        if (!File.Exists(filePath)) return new List<CalculationResult>();
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<CalculationResult>>(json) ?? new List<CalculationResult>();
    }
}
