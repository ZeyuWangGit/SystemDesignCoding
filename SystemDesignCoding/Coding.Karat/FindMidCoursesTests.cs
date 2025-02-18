namespace Coding.Karat;

using System;
using System.Collections.Generic;
using Xunit;

public class FindMidCoursesTests
{
    [Fact]
    public void TestFindMidCourses()
    {
        var allCourses = new List<string[]>
        {
            new string[] { "Logic", "COBOL" },
            new string[] { "Data Structures", "Algorithms" },
            new string[] { "Creative Writing", "Data Structures" },
            new string[] { "Algorithms", "COBOL" },
            new string[] { "Intro to Computer Science", "Data Structures" },
            new string[] { "Logic", "Compilers" },
            new string[] { "Data Structures", "Logic" },
            new string[] { "Creative Writing", "System Administration" },
            new string[] { "Databases", "System Administration" },
            new string[] { "Creative Writing", "Databases" },
            new string[] { "Intro to Computer Science", "Graphics" }
        };

        var expected = new HashSet<string>
        {
            "Data Structures", "Creative Writing", "Databases", "Intro to Computer Science"
        };

        var result = CoursePathFinder.FindMidCourses(allCourses);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFindMidCourses_SimpleCase()
    {
        var allCourses = new List<string[]>
        {
            new string[] { "CS101", "CS102" },
            new string[] { "CS102", "CS103" },
            new string[] { "CS103", "CS104" }
        };

        var expected = new HashSet<string> { "CS102" };

        var result = CoursePathFinder.FindMidCourses(allCourses);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFindMidCourses_NoCourses()
    {
        var allCourses = new List<string[]>();

        var expected = new HashSet<string>();

        var result = CoursePathFinder.FindMidCourses(allCourses);

        Assert.Equal(expected, result);
    }
}
