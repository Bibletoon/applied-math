using System.Text;
using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;

namespace Lab2.Tools;

public class ReportGenerator
{
    private readonly string _rootFolder;

    public ReportGenerator(string rootFolder)
    {
        _rootFolder = rootFolder;
        if (Directory.Exists(_rootFolder))
            Directory.Delete(_rootFolder, true);    
        Directory.CreateDirectory(_rootFolder);
    }

    public void Generate(IEnumerable<OptimizationTask> tasks)
    {
        foreach (var task in tasks)
        {
            string graphName =
                $"{task.MethodFullName} for {task.Request.Function.ToString()}";

            PlotModel contourSeriesModel = GenerateContourSeries(graphName, task.Request.Function.Invoke, task.Result.PointsHistory.ToList());

            string folderPath = Path.Combine(_rootFolder, task.Request.Function.ToString(), task.MethodName,  task.MethodFullName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var startPointString = $"{task.Request.StartPoint[0]}_{task.Request.StartPoint[1]}";
            
            string graphPath = Path.Combine(folderPath, $"graph_{startPointString}.svg");

            using FileStream stream = File.Create(graphPath);
            
            SvgExporter.Export(contourSeriesModel, stream, 1200, 1200, false);

            var reportPath = Path.Combine(folderPath, "report.md");

            if (File.Exists(reportPath))
            {
                var reportAddition = GenerateReportTableLine(
                    task.Request.StartPoint,
                    task.Result.Point,
                    task.Result.PointsHistory.Count);
                File.AppendAllText(reportPath, reportAddition);
            }
            else
            {
                var report = GenerateReport(
                    graphName, 
                    task.Request.StartPoint,
                    task.Result.Point,
                    task.Result.PointsHistory.Count);

                File.WriteAllText(reportPath, report);
            }
        }
    }
    

    private PlotModel GenerateContourSeries(string name, Func<Vector<double>, double> function, List<Vector<double>> points)
    {
        var model = new PlotModel() { Title = name };

        double x0 = points.Select(p => p[0]).Min();
        double x1 = points.Select(p => p[0]).Max();
        
        double y0 = points.Select(p => p[1]).Min();
        double y1 = points.Select(p => p[1]).Max();

        x0 = x0 * (Math.Sign(x0) == -1 ? 1.2 : 0.8);
        y0 = y0 * (Math.Sign(y0) == -1 ? 1.2 : 0.8);

        x1 = x1 * (Math.Sign(x1) == -1 ? 0.8 : 1.2);
        y1 = y1 * (Math.Sign(y1) == -1 ? 0.8 : 1.2);

        Func<double, double, double> peaks = (x, y) => function.Invoke(new DenseVector(new []{x, y}));

        var xx = ArrayBuilder.CreateVector(x0, x1, 500);
        var yy = ArrayBuilder.CreateVector(y0, y1, 500);
        var peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

        var cs = new ContourSeries()
        {
            Color = OxyColors.Black,
            LabelBackground = OxyColors.White,
            ColumnCoordinates = xx,
            RowCoordinates = yy,
            Data = peaksData
        };

        var line = new LineSeries()
        {
            LineStyle = LineStyle.Dash,
            StrokeThickness = 1,
            MarkerSize = 2,
            MarkerType = MarkerType.Circle
        };

        points.ForEach(p => line.Points.Add(new DataPoint(p[0], p[1])));
        
        model.Series.Add(cs);
        model.Series.Add(line);
        return model;
    }

    private string GenerateReport(string name, Vector<double> startPoint, Vector<double> result, int pointsCount)
    {
        var reportSb = new StringBuilder();
        reportSb.AppendLine($"## {name}");
        reportSb.AppendLine();
        reportSb.Append(GenerateReportTableLine(startPoint, result, pointsCount));
        return reportSb.ToString();
    }

    private string GenerateReportTableLine(Vector<double> startPoint, Vector<double> result, int pointsCount)
    {
        return $"| {startPoint[0]} | {startPoint[1]} | ({result[0]},{result[1]}) | {pointsCount - 1} |\n";
    }
}