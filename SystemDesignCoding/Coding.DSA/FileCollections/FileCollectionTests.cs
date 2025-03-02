namespace Coding.DSA.FileCollections;

public class FileCollectionTests
{
    [Fact]
    public void FileCollection_ShouldReturnCorrectCollectionSize()
    {
        var recorder = new FileCollection();
        recorder.AddFile(new FileRecord("file1.txt", 100));
        recorder.AddFile(new FileRecord("file2.txt", 100));
        Assert.Equal(200, recorder.GetTotalSize());
    }

    [Fact]
    public void FileCollection_ShouldHandleFileBelongsToMultipleCollections()
    {
        var recorder = new FileCollection();
        recorder.AddFile(new FileRecord("file1.txt", 100));
        recorder.AddFile(new FileRecord("file2.txt", 200, ["collection1"]));
        recorder.AddFile(new FileRecord("file3.txt", 200, ["collection1"]));
        recorder.AddFile(new FileRecord("file4.txt", 300, ["collection2", "collection1"]));

        var topCollections = recorder.GetTopNCollections(2);
        Assert.Equal(new List<string> { "collection1-700", "collection2-300" }, topCollections);
    }

    [Fact]
    public void FileCollection_ShouldHandleCollectionsHierarchy()
    {
        var recorder = new FileCollection();
        recorder.AddCollectionHierarchy("collection1", "collection2");
        recorder.AddFile(new FileRecord("file1.txt", 100));
        recorder.AddFile(new FileRecord("file2.txt", 200, ["collection1"]));
        recorder.AddFile(new FileRecord("file3.txt", 200, ["collection2"]));
        
        var topCollections = recorder.GetTopNCollections(2);
        Assert.Equal(new List<string> { "collection2-400", "collection1-200" }, topCollections);
    }
}