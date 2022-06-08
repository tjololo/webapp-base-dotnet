using Tjololo.DI.Interfaces;

namespace Tjololo.DI.Defaults;

public class SimpleCalculator: PercentageCalculator
{

    private readonly int _percentage;
    
    public SimpleCalculator()
    {
        _percentage = 10;
    }
    /// <inheritdoc />
    public double AddPercentage(double number)
    {
        return number + (number * _percentage / 100);
    }
}