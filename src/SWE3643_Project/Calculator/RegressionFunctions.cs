namespace Console;

public static class RegressionFunctions
{
    public static void SingleLinearRegression(ValuePair[] values, out double slope, out double intercept)
    {
        if (values.Length < 2)
        {
            throw new ArgumentException("Must pass at least two value pairs!");
        }
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
        foreach (ValuePair value in values)
        {
            sumX += value.X;
            sumY += value.Y;
            sumXY += value.X * value.Y;
            sumX2 += Math.Pow(value.X, 2);
        }
        intercept = (sumY * sumX2 - sumX * sumXY) / (values.Length * sumX2 - Math.Pow(sumX, 2));
        slope = (values.Length * sumXY-sumX * sumY) / (values.Length * sumX2 - Math.Pow(sumX, 2));
    }
    public static double PredictYLinearRegression(double x, double slope, double intercept)
    {
        return slope * x + intercept;
    }
}

public class ValuePair
{
    public double X { get; set; }
    public double Y { get; set; }
    public ValuePair(double x, double y)
    {
        X = x;
        Y = y;
    }
}