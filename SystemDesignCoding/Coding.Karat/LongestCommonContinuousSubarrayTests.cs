namespace Coding.Karat;

using System;
using System.Collections.Generic;
using Xunit;

public class LongestCommonContinuousSubarrayTests
{
    [Fact]
    public void Test_Case1()
    {
        var history1 = new List<string> { "3234.html", "xys.html", "7hsaa.html" };
        var history2 = new List<string> { "3234.html", "sdhsfjdsh.html", "xys.html", "7hsaa.html" };
        var expected = new List<string> { "xys.html", "7hsaa.html" };
        
        var result = LongestCommonContinuousSubarrayCode.LongestCommonContinuousSubarray(history1, history2);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Case2_NoCommon()
    {
        var history1 = new List<string> { "a.html", "b.html", "c.html" };
        var history2 = new List<string> { "x.html", "y.html", "z.html" };
        var expected = new List<string>();
        
        var result = LongestCommonContinuousSubarrayCode.LongestCommonContinuousSubarray(history1, history2);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Case3_EntireMatch()
    {
        var history1 = new List<string> { "a.html", "b.html", "c.html" };
        var history2 = new List<string> { "a.html", "b.html", "c.html" };
        var expected = new List<string> { "a.html", "b.html", "c.html" };
        
        var result = LongestCommonContinuousSubarrayCode.LongestCommonContinuousSubarray(history1, history2);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Case4_SubsetMatch()
    {
        var history1 = new List<string> { "1.html", "2.html", "3.html", "4.html" };
        var history2 = new List<string> { "3.html", "4.html" };
        var expected = new List<string> { "3.html", "4.html" };
        
        var result = LongestCommonContinuousSubarrayCode.LongestCommonContinuousSubarray(history1, history2);
        
        Assert.Equal(expected, result);
    }
}