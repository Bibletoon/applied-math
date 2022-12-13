using OfficeOpenXml;

namespace Lab3.Models;

public class SpreadsheetRowComponent
{
    private readonly DrawComponent _drawComponent;

    public SpreadsheetRowComponent(DrawComponent drawComponent, int width, int height, int rowNumber)
    {
        _drawComponent = drawComponent;
        Width = width;
        Height = height;
        RowNumber = rowNumber;
    }

    public delegate void DrawComponent(ExcelWorksheet worksheet, int row, int column);

    public int Width { get; }
    public int Height { get; }
    public int RowNumber { get; }

    public void Draw(ExcelWorksheet worksheet, int row, int column)
        => _drawComponent(worksheet, row, column);
}