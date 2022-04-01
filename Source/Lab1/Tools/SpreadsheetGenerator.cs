using System.Globalization;
using Lab1.OptimizationContexts;
using Lab1.Results;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace Lab1.Tools;

public class SpreadsheetGenerator
{
    private delegate (int, int) AddRunnerResult(ExcelWorksheet worksheet, int row, int column);

    private readonly List<AddRunnerResult> _addRunnerResults = new List<AddRunnerResult>();

    public SpreadsheetGenerator AddOptimizationResultToSpreadsheet<TContext>(OptimisationResult<TContext> result)
        where TContext : IOptimizationContext
    {
        AddRunnerResult addRunnerResult = (ws, row, column) =>
        {
            ws.Cells[row, column].Value = result.OptimisationMethod.Title;

            ws.Cells[row + 1, column].Value = "Accuracy";
            ws.Cells[row + 1, column + 1].Value = "Iteration Count";
            ws.Cells[row + 1, column + 2].Value = "Function Call Count";
            ws.Cells[row + 1, column + 3].Value = "Result";

            for (int i = 0; i < result.RunResults.Count; i++)
            {
                var runResult = result.RunResults[i];

                ws.Cells[row + 2 + i, column].Value = runResult.Accuracy;
                ws.Cells[row + 2 + i, column + 1].Value = runResult.IterationCount;
                ws.Cells[row + 2 + i, column + 2].Value = runResult.FunctionCallCount;
                ws.Cells[row + 2 + i, column + 3].Value = runResult.Result;
            }

            var chart = ws.Drawings.AddLineChart(result.OptimisationMethod.Title, eLineChartType.Line);
            chart.SetPosition(row - 1, 0, column + 4, 0);
            
            chart.Axis[0].Title.Text = "Iteration Number";
            chart.Axis[1].Title.Text = "Border Value";

            foreach (var runResult in result.RunResults)
            {
                var name = $"{result.OptimisationMethod.Title} - {runResult.Accuracy}";
                var iterationIntervalWorksheet = ws.Workbook.Worksheets.Add(name);

                for (int i = 0; i < runResult.Intervals.Count; i++)
                {
                    var interval = runResult.Intervals[i];
                    iterationIntervalWorksheet.Cells[i + 1, 1].Value = i + 1;
                    iterationIntervalWorksheet.Cells[i + 1, 2].Value = interval.A;
                    iterationIntervalWorksheet.Cells[i + 1, 3].Value = interval.B;
                }

                var x = iterationIntervalWorksheet.Cells[1, 1, runResult.Intervals.Count, 1];

                var a = iterationIntervalWorksheet.Cells[1, 2, runResult.Intervals.Count, 2];
                var b = iterationIntervalWorksheet.Cells[1, 3, runResult.Intervals.Count, 3];

                var aSeries = chart.Series.Add(a, x);
                var bSeries = chart.Series.Add(b, x);

                aSeries.Header = $"{runResult.Accuracy.ToString(CultureInfo.InvariantCulture)} - A";
                bSeries.Header = $"{runResult.Accuracy.ToString(CultureInfo.InvariantCulture)} - B";
            }

            return (row + 9, column);
        };

        _addRunnerResults.Add(addRunnerResult);
        return this;
    }

    public void Build(string path)
    {
        var package = new ExcelPackage(path);
        var workbook = package.Workbook;
        var worksheet = workbook.Worksheets.Add("Optimization Results");

        var row = 1;
        var column = 1;

        foreach (var addRunnerResult in _addRunnerResults)
        {
            (row, column) = addRunnerResult(worksheet, row, column);
            row += 1;
        }

        package.Save();
    }
}