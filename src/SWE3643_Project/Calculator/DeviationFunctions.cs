namespace Console;

public static class DeviationFunctions
{
    public static double SampleStandardDeviation(double[] values)
    {
        if(values.Length < 2)
        {
            throw new ArgumentException("Must pass at least two values!");
        }
        double average = Mean(values);
        double sum = 0;
        foreach (double value in values)
        {
            sum += Math.Pow(value - average, 2);
        }
        return Math.Sqrt(sum / (values.Length - 1));
    }

    public static double PopulationStandardDeviation(double[] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentException("Must pass at least one value!");
        }
        double average = Mean(values);
        double sum = 0;
        foreach (double value in values)
        {
            sum += Math.Pow(value - average, 2);
        }
        return Math.Sqrt(sum / values.Length);
    }
    public static double Mean(double[] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentException("Must pass at least one value!");
        }
        double sum = 0;
        foreach (double value in values)
        {
            sum += value;
        }
        return sum / values.Length;
    }
    public static double ZScore(double value, double mean, double standardDeviation)
    {
        if(standardDeviation == 0) throw new ArgumentException("Standard deviation cannot be 0!");
        return (value - mean) / standardDeviation;
    }
}