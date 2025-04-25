using Microsoft.Win32;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using ColdProductionCalculator.Models;

namespace ColdProductionCalculator.Services;

public static class ExportService
{
    public static void ExportToPdf(CalculationResult result)
    {
        SaveFileDialog saveDialog = new SaveFileDialog
        {
            Filter = "PDF файлы (*.pdf)|*.pdf",
            FileName = $"Расчёт_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
        };

        if (saveDialog.ShowDialog() == true)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);

            int y = 40;
            void Print(string text)
            {
                gfx.DrawString(text, font, XBrushes.Black, new XRect(20, y, page.Width, page.Height));
                y += 20;
            }

            Print($"Коров: {result.CowCount}");
            Print($"Удой: {result.MilkPerCow} л");
            Print($"Температура: {result.FinalTemp} °C");
            Print($"Мощность: {result.PowerKw} кВт");
            Print($"Время: {result.WorkHours} ч");
            Print($"Qt: {result.Qt:F2} кДж");
            Print($"Qp: {result.Qp:F2} кДж");
            Print($"Энергия: {result.EnergyConsumption:F2} кВт·ч");

            document.Save(saveDialog.FileName);
        }
    }
}
