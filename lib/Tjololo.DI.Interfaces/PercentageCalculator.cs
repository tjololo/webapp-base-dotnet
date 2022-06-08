namespace Tjololo.DI.Interfaces;

public interface PercentageCalculator
{
    /// <summary>
    /// Add the percentage to a number
    /// </summary>
    /// <param name="number">Number to add the percentage to</param>
    /// <returns>The number with the percentage added</returns>
    double AddPercentage(double number);
}