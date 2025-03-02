namespace Coding.DSA.FileCollections;

public class FileRecord
{
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public List<string> Collections { get; set; }

    public FileRecord(string fileName, int fileSize, List<string>? collections = null)
    {
        FileName = fileName;
        FileSize = fileSize;
        Collections = collections ?? [];
    }
}