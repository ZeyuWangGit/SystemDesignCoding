namespace Coding.DSA.FileCollections;

public interface IFileCollection
{
    void AddCollectionHierarchy(string collection, string parentCollection);
    void AddFile(FileRecord record);
    int GetTotalSize();
    List<string> GetTopNCollections(int n);
}