using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;

namespace Lab2.Tools;

public class GraphicGenerator
{
    public static PlotModel GenerateContourSeries(Func<Vector<double>, double> function, List<Vector<double>> points)
    {
        var model = new PlotModel() { Title = "ContourSeries" };

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
}