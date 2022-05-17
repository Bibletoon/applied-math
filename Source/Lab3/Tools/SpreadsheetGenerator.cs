using Lab3.Models;
using OfficeOpenXml;

namespace Lab3.Tools;

public class SpreadsheetGenerator
{
    private readonly List<SpreadsheetRowComponent> _components = new List<SpreadsheetRowComponent>();

    public SpreadsheetGenerator Enqueue(SolutionRunnerResult result, int rowNumber, bool showVectors = true)
    {
        void Draw(ExcelWorksheet ws, int row, int column)
        {
            var normActual = result.Actual.L1Norm();
            var normReceived = result.Received.L1Norm();

            var absoluteError = Math.Abs(normActual - normReceived);
            var relativeError = absoluteError / normActual;

            ws.Cells[row, column].Value = result.MethodTitle;
            ws.Cells[row, column + 1].Value = result.MatrixTitle;

            var tempColumn = column;

            if (showVectors)
            {
                ws.Cells[row + 1, tempColumn++].Value = "Initially approximated X";
                ws.Cells[row + 1, tempColumn++].Value = "Actual X";
                ws.Cells[row + 1, tempColumn++].Value = "Solved X";
            }

            ws.Cells[row + 1, tempColumn++].Value = "Accuracy";
            ws.Cells[row + 1, tempColumn++].Value = "K";
            ws.Cells[row + 1, tempColumn++].Value = "N";
            ws.Cells[row + 1, tempColumn++].Value = "Iteration count";
            ws.Cells[row + 1, tempColumn++].Value = "Absolute error";
            ws.Cells[row + 1, tempColumn].Value = "Relative error";

            tempColumn = column;

            if (showVectors)
            {
                for (var i = 0; i < result.Actual.Count; i++)
                {
                    ws.Cells[row + 2 + i, tempColumn].Value = result.InitialApproximation[i];
                    ws.Cells[row + 2 + i, tempColumn + 1].Value = result.Actual[i];
                    ws.Cells[row + 2 + i, tempColumn + 2].Value = result.Received[i];
                }

                tempColumn += 2;
            }


            ws.Cells[row + 2, tempColumn++].Value = result.Accuracy;
            ws.Cells[row + 2, tempColumn++].Value = result.K;
            ws.Cells[row + 2, tempColumn++].Value = result.Actual.Count;
            ws.Cells[row + 2, tempColumn++].Value = result.IterationCount;
            ws.Cells[row + 2, tempColumn++].Value = absoluteError;
            ws.Cells[row + 2, tempColumn].Value = relativeError;
        }

        var component = new SpreadsheetRowComponent(
            Draw,
            showVectors ? 9 : 6,
            showVectors ? result.Actual.Count + 2 : 2,
            rowNumber);
        _components.Add(component);

        return this;
    }

    public void Build(string path, string sheetName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage(path);
        var workbook = package.Workbook;
        var worksheet = workbook.Worksheets.Add(sheetName);

        IOrderedEnumerable<IGrouping<int, SpreadsheetRowComponent>> results = _components
            .GroupBy(c => c.RowNumber)
            .OrderBy(g => g.Key);

        var row = 1;
        var column = 1;
        var maxHeight = 0;

        foreach (IGrouping<int, SpreadsheetRowComponent> groping in results)
        {
            foreach (var component in groping)
            {
                component.Draw(worksheet, row, column);
                maxHeight = Math.Max(maxHeight, component.Height);
                column += component.Width + 1;
            }

            row += maxHeight + 2;
            maxHeight = 0;
            column = 1;
        }

        package.Save();
    }

    public void ClearQueue()
        => _components.Clear();
}