namespace Coding.Karat;

using System.Collections.Generic;
using Xunit;

public class CarpoolTests
{
    [Fact]
    public void Test_Carpool_Scenario1()
    {
        var roads = new List<(string Origin, string Destination, int Duration)>
        {
            ("Bridgewater", "Caledonia", 30),
            ("Caledonia", "New Grafton", 15),
            ("New Grafton", "Campground", 5),
            ("Liverpool", "Milton", 10),
            ("Milton", "New Grafton", 30)
        };

        var starts = new List<string> { "Bridgewater", "Liverpool" };

        var people = new List<(string Name, string Location)>
        {
            ("Jessie", "Bridgewater"),
            ("Travis", "Caledonia"),
            ("Jeremy", "New Grafton"),
            ("Katie", "Liverpool")
        };

        var result = Carpool.AssignPassengers(roads, starts, people);

        var expectedCar1 = new List<string> { "Jessie", "Travis", "Jeremy" };
        var expectedCar2 = new List<string> { "Katie" };

        Assert.Contains(expectedCar1, result);
        Assert.Contains(expectedCar2, result);
    }

    [Fact]
    public void Test_Carpool_Scenario2()
    {
        var roads = new List<(string Origin, string Destination, int Duration)>
        {
            ("Riverport", "Chester", 40),
            ("Chester", "Campground", 60),
            ("Halifax", "Chester", 40)
        };

        var starts = new List<string> { "Riverport", "Halifax" };

        var people = new List<(string Name, string Location)>
        {
            ("Colin", "Riverport"),
            ("Sam", "Chester"),
            ("Alyssa", "Halifax")
        };

        var result = Carpool.AssignPassengers(roads, starts, people);

        var expected1 = new List<string> { "Colin", "Sam" };
        var expected2 = new List<string> { "Alyssa" };

        Assert.Contains(expected1, result);
        Assert.Contains(expected2, result);
    }

    [Fact]
    public void Test_Carpool_Scenario3()
    {
        var roads = new List<(string Origin, string Destination, int Duration)>
        {
            ("Riverport", "Bridgewater", 1),
            ("Bridgewater", "Liverpool", 1),
            ("Liverpool", "Campground", 1)
        };

        var starts = new List<string> { "Riverport", "Bridgewater" };

        var people = new List<(string Name, string Location)>
        {
            ("Colin", "Riverport"),
            ("Jessie", "Bridgewater"),
            ("Sam", "Liverpool")
        };

        var result = Carpool.AssignPassengers(roads, starts, people);

        var expected1 = new List<string> { "Colin" };
        var expected2 = new List<string> { "Jessie", "Sam" };

        Assert.Contains(expected1, result);
        Assert.Contains(expected2, result);
    }
}
