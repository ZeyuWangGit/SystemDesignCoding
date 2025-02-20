namespace Coding.Karat;

public class CarpoolTests
{
    [Fact]
    public void TestAssignPassengers()
    {
        var roads1 = new List<(string Origin, string Destination, int Duration)>
        {
            ("Bridgewater", "Caledonia", 30),
            ("Caledonia", "New Grafton", 15),
            ("New Grafton", "Campground", 5),
            ("Liverpool", "Milton", 10),
            ("Milton", "New Grafton", 30)
        };

        var starts1 = new List<string> { "Bridgewater", "Liverpool" };

        var people1 = new List<(string Name, string Location)>
        {
            ("Jessie", "Bridgewater"),
            ("Travis", "Caledonia"),
            ("Jeremy", "New Grafton"),
            ("Katie", "Liverpool")
        };

        var result = Carpool.AssignPassengers(roads1, starts1, people1);

        var expectedCar1 = new List<string> { "Jessie", "Travis" };
        var expectedCar2 = new List<string> { "Katie", "Jeremy" };

        Assert.Contains(expectedCar1, result);
        Assert.Contains(expectedCar2, result);
    }
}