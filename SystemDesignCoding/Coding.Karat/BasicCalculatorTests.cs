namespace Coding.Karat;

public class BasicCalculatorTests
{
    [Theory]
    [InlineData("2+3-999", -994)]
    [InlineData("10+20+30", 60)]
    [InlineData("100-50+25", 75)]
    [InlineData("7", 7)]
    [InlineData("", 0)]
    public void BasicCalculatorCode_ShouldReturnCorrectResult(string expression, int expected)
    {
        var result = BasicCalculatorCode.BasicCalculator(expression);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("2+(3-999)", -994)]
    [InlineData("(10+(20+30))", 60)]
    [InlineData("((100-50)+25)", 75)]
    [InlineData("7", 7)]
    [InlineData("", 0)]
    [InlineData("1+(2-(3+4))", -4)]
    public void BasicCalculatorWithParenthesis_ShouldReturnCorrectResult(string expression, int expected)
    {
        var result = BasicCalculatorCode.BasicCalculatorWithParenthesis(expression);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("a+b+c+1", "7")]
    [InlineData("a+b+c+1+d", "7+d")]
    [InlineData("x+y+z+5", "35")]
    [InlineData("p+q+r-2+t", "13+t")]
    [InlineData("a+b+d+e+3", "6+d+e")]
    public void BasicCalculatorWithVariables_ShouldReturnCorrectResult(string expression, string expected)
    {
        var variables = new Dictionary<string, int>
        {
            { "a", 1 }, { "b", 2 }, { "c", 3 },
            { "x", 10 }, { "y", 10 }, { "z", 10 },
            { "p", 5 }, { "q", 5 }, { "r", 5 }
        };

        var result = BasicCalculatorCode.BasicCalculatorWithVariables(expression, variables);
        Assert.Equal(expected, result);
    }
}