namespace Coding.DataStructure.FileCollections;

public class BasicFileCollectionServiceTests
{
    [Fact]
    public void BasicFileCollectionService_CalculateTotalSize()
    {
        var tracker = new BasicFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["collection2"]);
        tracker.AddFile("file4", 400, ["collection2"]);
        Assert.Equal(1000, tracker.GetTotalSize());
    }
    
    [Fact]
    public void BasicFileCollectionService_GetTopNCollections()
    {
        var tracker = new BasicFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["collection2"]);
        tracker.AddFile("file4", 400, ["collection2"]);
        tracker.AddFile("file4", 800, ["collection3"]);
        List<string> expected = ["collection3-800", "collection2-700"];
        Assert.Equal(expected, tracker.GetTopNCollections(2));
    }
    
    [Fact]
    public void BasicFileCollectionService_GetTopNCollectionsWithSameSize()
    {
        var tracker = new BasicFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["acollecton"]);
        tracker.AddFile("file4", 400, ["acollecton"]);
        tracker.AddFile("file4", 700, ["bcollection"]);
        tracker.AddFile("file5", 700, ["ccollection"]);
        List<string> expected = ["acollecton-700", "bcollection-700"];
        Assert.Equal(expected, tracker.GetTopNCollections(2));
    }
}