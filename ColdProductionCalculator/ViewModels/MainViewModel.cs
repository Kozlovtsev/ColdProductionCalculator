using ColdProductionCalculator.Helpers;
using ColdProductionCalculator.Models;
using ColdProductionCalculator.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ColdProductionCalculator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _result;
    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged(nameof(Result));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ObservableCollection<CalculationResult> History { get; set; } = new(JsonStorage.LoadHistory());

    public int CowCount { get => _cowCount; set { _cowCount = value; OnPropertyChanged(nameof(CowCount)); } }
    public double MilkPerCow { get => _milkPerCow; set { _milkPerCow = value; OnPropertyChanged(nameof(MilkPerCow)); } }
    public double FinalTemp { get => _finalTemp; set { _finalTemp = value; OnPropertyChanged(nameof(FinalTemp)); } }
    public double PowerKw { get => _powerKw; set { _powerKw = value; OnPropertyChanged(nameof(PowerKw)); } }
    public double WorkHours { get => _workHours; set { _workHours = value; OnPropertyChanged(nameof(WorkHours)); } }

    public double Qt { get => _qt; set { _qt = value; OnPropertyChanged(nameof(Qt)); } }
    public double Qp { get => _qp; set { _qp = value; OnPropertyChanged(nameof(Qp)); } }
    public double Energy { get => _energy; set { _energy = value; OnPropertyChanged(nameof(Energy)); } }

    public ISeries[] ChartSeries { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    private int _cowCount;
    private double _milkPerCow, _finalTemp, _powerKw, _workHours;
    private double _qt, _qp, _energy;

    private CalculationResult? _lastResult;

    public ICommand CalculateCommand => new RelayCommand(_ =>
    {
        var mass = CowCount * MilkPerCow;
        Qt = CalculationService.CalculateQt(mass, FinalTemp);
        Qp = CalculationService.CalculateQp(Qt, 500);
        Energy = CalculationService.CalculateEnergy(PowerKw, WorkHours);

        _lastResult = new CalculationResult
        {
            Timestamp = DateTime.Now,
            CowCount = CowCount,
            MilkPerCow = MilkPerCow,
            FinalTemp = FinalTemp,
            PowerKw = PowerKw,
            WorkHours = WorkHours,
            Qt = Qt,
            Qp = Qp,
            EnergyConsumption = Energy
        };

        History.Add(_lastResult);
        JsonStorage.SaveHistory(History.ToList());

        var data = CalculationService.GenerateQtGraph(mass);
        ChartSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = data.Select(d => d.Qt).ToArray(),
                Name = "Qt"
            }
        };
        XAxes = new Axis[]
        {
            new Axis { Labels = data.Select(d => d.Temperature.ToString("F0")).ToArray(), Name = "Температура (°C)" }
        };
        YAxes = new Axis[]
        {
            new Axis { Name = "Qt (кДж)" }
        };

        OnPropertyChanged(nameof(ChartSeries));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    });

    public ICommand ExportToPdfCommand => new RelayCommand(_ =>
    {
        if (_lastResult != null)
            ExportService.ExportToPdf(_lastResult);
    });
}
