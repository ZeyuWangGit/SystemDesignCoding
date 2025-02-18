namespace Coding.Karat;

using System;
using System.Collections.Generic;
using Xunit;

public class SharedCoursesFinderTests
{
    [Fact]
    public void TestFindPairs()
    {
        var studentCoursePairs = new List<string[]>
        {
            new string[] { "58", "Software Design" },
            new string[] { "58", "Linear Algebra" },
            new string[] { "94", "Art History" },
            new string[] { "94", "Operating Systems" },
            new string[] { "17", "Software Design" },
            new string[] { "58", "Mechanics" },
            new string[] { "58", "Economics" },
            new string[] { "17", "Linear Algebra" },
            new string[] { "17", "Political Science" },
            new string[] { "94", "Economics" },
            new string[] { "25", "Economics" }
        };

        var expected = new Dictionary<Tuple<string, string>, HashSet<string>>
        {
            { new Tuple<string, string>("58", "94"), new HashSet<string> { "Economics" } },
            { new Tuple<string, string>("58", "17"), new HashSet<string> { "Software Design", "Linear Algebra" } },
            { new Tuple<string, string>("58", "25"), new HashSet<string> { "Economics" } },
            { new Tuple<string, string>("94", "17"), new HashSet<string>() },
            { new Tuple<string, string>("94", "25"), new HashSet<string> { "Economics" } },
            { new Tuple<string, string>("17", "25"), new HashSet<string>() }
        };

        var result = SharedCoursesFinderCode.FindPairs(studentCoursePairs);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFindPairs_NoSharedCourses()
    {
        var studentCoursePairs = new List<string[]>
        {
            new string[] { "42", "Software Design" },
            new string[] { "0", "Advanced Mechanics" },
            new string[] { "9", "Art History" }
        };

        var expected = new Dictionary<Tuple<string, string>, HashSet<string>>
        {
            { new Tuple<string, string>("42", "0"), new HashSet<string>() },
            { new Tuple<string, string>("42", "9"), new HashSet<string>() },
            { new Tuple<string, string>("0", "9"), new HashSet<string>() }
        };

        var result = SharedCoursesFinderCode.FindPairs(studentCoursePairs);

        Assert.Equal(expected, result);
    }
}
