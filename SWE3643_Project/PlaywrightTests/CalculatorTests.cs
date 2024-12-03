using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Console;
using NUnit.Framework.Constraints;
using ArgumentException = System.ArgumentException;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CalculatorTests
{
    [Test]
    public void ComputeSampleStandardDeviation_ValidList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-2
        
        // Arrange
        var values = new double[] { 9, 8, 7, 6, 5 };

        Assert.That(Equals(1.5811388300841898, DeviationFunctions.SampleStandardDeviation(values)));
    }
    
    [Test]
    public void ComputeSampleStandardDeviation_ZerosList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-2
        
        // Arrange
        var values = new double[] { 0, 0, 0 };

        Assert.That(Equals(0.0, DeviationFunctions.SampleStandardDeviation(values)));
    }
    
    [Test]
    public void ComputeSampleStandardDeviation_InvalidList_ThrowsError()
    {
        //preq-UNIT-TEST-2
        
        // Arrange
        var values = new double[0];

        Assert.Throws<ArgumentException>(() => DeviationFunctions.SampleStandardDeviation(values), "Must pass at least two values!");
    }
    
    [Test]
    public void ComputeSampleStandardDeviation_SingleValue_ThrowsError()
    {
        //preq-UNIT-TEST-2
        
        // Arrange
        var values = new double[] {7};

        Assert.Throws<ArgumentException>(() => DeviationFunctions.SampleStandardDeviation(values), "Must pass at least one value!");
    }
    
    [Test]
    public void ComputePopulationStandardDeviation_ValidList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-3
        
        // Arrange
        var values = new double[] { 9, 8, 7, 6, 5 };

        Assert.That(Equals(1.4142135623730951, DeviationFunctions.PopulationStandardDeviation(values)));
    }
    
    [Test]
    public void ComputePopulationStandardDeviation_ZerosList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-3
        
        // Arrange
        var values = new double[] { 0, 0, 0 };

        Assert.That(Equals(0.0, DeviationFunctions.PopulationStandardDeviation(values)));
    }
    
    [Test]
    public void ComputePopulationStandardDeviation_InvalidList_ThrowsError()
    {
        //preq-UNIT-TEST-3
        
        // Arrange
        var values = new double[0];

        Assert.Throws<ArgumentException>(() => DeviationFunctions.PopulationStandardDeviation(values), "Must pass at least one value!");
    }
    
    [Test]
    public void ComputeMean_ValidList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-4
        
        // Arrange
        var values = new double[] { 9, 8, 7, 6, 5 };

        Assert.That(Equals(7.0, DeviationFunctions.Mean(values)));
    }
    [Test]
    public void ComputeMean_InvalidList_ThrowsError()
    {
        //preq-UNIT-TEST-4
        
        // Arrange
        var values = new double[0];

        Assert.Throws<ArgumentException>(() => DeviationFunctions.Mean(values), "Must pass at least one value!");
    }
    
    [Test]
    public void ComputeZScore_ValidInput_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-5
        
        // Arrange
        Assert.That(Equals(2.846049894151541, DeviationFunctions.ZScore(11.5, 7, 1.5811388300841898)));
    }
    [Test]
    public void ComputeZScore_EmptyInput_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-5
        
        // Arrange
        Assert.That(Equals(-1.5, DeviationFunctions.ZScore(0, 3, 2)));
    }
    [Test]
    public void ComputeZScore_EmptySD_ThrowsError()
    {
        //preq-UNIT-TEST-5
        
        // Arrange
        Assert.Throws<ArgumentException>(() => DeviationFunctions.ZScore(5, 3, 0), "Standard deviation cannot be 0!");
    }
    
    [Test]
    public void ComputeSingleLinearRegression_ValidList_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-6
        
        // Arrange
        var values = new ValuePair[] { 
            new ValuePair(1.47,52.21),
            new ValuePair(1.5,53.12),
            new ValuePair(1.52,54.48),
            new ValuePair(1.55,55.84),
            new ValuePair(1.57,57.2),
            new ValuePair(1.6,58.57),
            new ValuePair(1.63,59.93),
            new ValuePair(1.65,61.29),
            new ValuePair(1.68,63.11),
            new ValuePair(1.7,64.47),
            new ValuePair(1.73,66.28),
            new ValuePair(1.75,68.1),
            new ValuePair(1.78,69.92),
            new ValuePair(1.8,72.19),
            new ValuePair(1.83,74.46), };

        double slope;
        double intercept;
        RegressionFunctions.SingleLinearRegression(values, out slope, out intercept);
        
        Assert.That(Equals(61.272186542107434, slope));
        Assert.That(Equals(-39.061955918841036, intercept));
    }
    
    [Test]
    public void ComputeSingleLinearRegression_EmptyList_ThrowsError()
    {
        //preq-UNIT-TEST-6
        
        // Arrange
        var values = new ValuePair[0];

        double slope;
        double intercept;
        Assert.Throws<ArgumentException>(() => RegressionFunctions.SingleLinearRegression(values, out slope, out intercept), "Must pass at least two value pairs!");
    }
    
    [Test]
    public void ComputeSingleLinearRegression_XsAreSame_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-6
        
        // Arrange
        var values = new ValuePair[] { 
            new ValuePair(1,1),
            new ValuePair(1,2),
            new ValuePair(1,3),
            new ValuePair(1,4),
            new ValuePair(1,5),
            new ValuePair(1,6),
            new ValuePair(1,7) };

        double slope;
        double intercept;
        RegressionFunctions.SingleLinearRegression(values, out slope, out intercept);
        
        Assert.That(double.IsNaN(slope));
        Assert.That(double.IsNaN(intercept));
    }
    
    [Test]
    public void ComputeSingleLinearRegression_YsAreSame_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-6
        
        // Arrange
        var values = new ValuePair[] { 
            new ValuePair(1,1),
            new ValuePair(2,1),
            new ValuePair(3,1),
            new ValuePair(4,1),
            new ValuePair(5,1),
            new ValuePair(6,1),
            new ValuePair(7,1) };

        double slope;
        double intercept;
        RegressionFunctions.SingleLinearRegression(values, out slope, out intercept);
        
        Assert.That(Equals(0.0, slope));
        Assert.That(Equals(1.0, intercept));
    }
    
    [Test]
    public void ComputeSingleLinearRegression_XYIsZeroZero_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-6
        
        // Arrange
        var values = new ValuePair[] { 
            new ValuePair(0,0),
            new ValuePair(0,0),
            new ValuePair(0,0) };

        double slope;
        double intercept;
        RegressionFunctions.SingleLinearRegression(values, out slope, out intercept);
        
        Assert.That(double.IsNaN(slope));
        Assert.That(double.IsNaN(intercept));
    }
    
    [Test]
    public void ComputePredictY_ValidInput_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-5
        
        // Arrange
        Assert.That(Equals(54.990850423296244, RegressionFunctions.PredictYLinearRegression(1.535,61.272186542107434, -39.061955918838656)));
    }
    
    [Test]
    public void ComputePredictY_EmptyInput_ReturnsCorrectResult()
    {
        //preq-UNIT-TEST-5
        
        // Arrange
        Assert.That(Equals(10.0, RegressionFunctions.PredictYLinearRegression(0,2, 10)));
    }
}