using Tjololo.DI.Interfaces;

namespace simple_overrides;

[Transient(typeof(PercentageCalculator))]
public class ACalcOverride : PercentageCalculator
{
    private readonly double _percentage = 25;

    public double AddPercentage(double prize)
    {
        return prize + (prize * _percentage / 100);
    }
}