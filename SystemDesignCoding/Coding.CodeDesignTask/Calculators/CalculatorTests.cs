namespace Coding.CodeDesignTask.Calculators;

public class CalculatorTests
{
    [Fact]
    public void CalculatorOne_Works()
    {
        var calculator = new CalculatorOne();
        Assert.Equal(10, calculator.Calculate("1+2+3+4"));
        Assert.Equal(2, calculator.Calculate("1+2+3-4"));
        Assert.Equal(0, calculator.Calculate("-1+2+3-4"));
    }
    
    [Fact]
    public void CalculatorTwo_Works()
    {
        var calculator = new CalculatorTwo();
        Assert.Equal(10, calculator.Calculate("1+2+3+4"));
        Assert.Equal(2, calculator.Calculate("1+2+3-4"));
        Assert.Equal(0, calculator.Calculate("-1+2+3-4"));
        
        Assert.Equal(14, calculator.Calculate("1*2+3*4"));
        Assert.Equal(4, calculator.Calculate("1*2+8/4"));
    }
    
    [Fact]
    public void CalculatorAdvanced_Works()
    {
        var calculator = new CalculatorAdvanced();
        Assert.Equal(10, calculator.Calculate("1+2+3+4"));
        Assert.Equal(2, calculator.Calculate("1+2+3-4"));
        Assert.Equal(0, calculator.Calculate("-1+2+3-4"));
        
        Assert.Equal(14, calculator.Calculate("1*2+3*4"));
        Assert.Equal(4, calculator.Calculate("1*2+8/4"));
        
        Assert.Equal(5, calculator.Calculate("1*(2+8)/2"));
    }
}