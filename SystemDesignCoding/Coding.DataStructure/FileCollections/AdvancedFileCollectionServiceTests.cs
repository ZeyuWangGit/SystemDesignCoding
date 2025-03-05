namespace Coding.DataStructure.FileCollections;

public class AdvancedFileCollectionServiceTests
{
    [Fact]
    public void AdvancedFileCollectionService_CalculateTotalSize()
    {
        var tracker = new AdvancedFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["collection2"]);
        tracker.AddFile("file4", 400, ["collection2"]);
        Assert.Equal(1000, tracker.GetTotalSize());
    }
    
    [Fact]
    public void AdvancedFileCollectionService_GetTopNCollections()
    {
        var tracker = new AdvancedFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["collection2"]);
        tracker.AddFile("file4", 400, ["collection2"]);
        tracker.AddFile("file4", 800, ["collection3"]);
        List<string> expected = ["collection3-800", "collection2-700"];
        Assert.Equal(expected, tracker.GetTopNCollections(2));
    }
    
    [Fact]
    public void AdvancedFileCollectionService_GetTopNCollectionsWithSameSize()
    {
        var tracker = new AdvancedFileCollectionService();
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["acollecton"]);
        tracker.AddFile("file4", 400, ["acollecton"]);
        tracker.AddFile("file4", 700, ["bcollection"]);
        tracker.AddFile("file5", 700, ["ccollection"]);
        List<string> expected = ["acollecton-700", "bcollection-700"];
        Assert.Equal(expected, tracker.GetTopNCollections(2));
    }
    
    [Fact]
    public void AdvancedFileCollectionService_CalculateTotalSizeWithHierarchy()
    {
        var tracker = new AdvancedFileCollectionService();
        tracker.AddCollectionHierarchy("collection1", "collection2");
        tracker.AddFile("file1", 100, []);
        tracker.AddFile("file2", 200, ["collection1"]);
        tracker.AddFile("file3", 300, ["collection2"]);
        tracker.AddFile("file4", 400, ["collection2"]);
        List<string> expected = ["collection2-900", "collection1-200"];
        Assert.Equal(expected, tracker.GetTopNCollections(2));
    }
}