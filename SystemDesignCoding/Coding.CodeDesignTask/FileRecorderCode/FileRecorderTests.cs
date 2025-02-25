namespace Coding.CodeDesignTask.FileRecorderCode;

public class FileRecorderTests
{
    [Fact]
        public void GetTotalSize_ShouldReturnCorrectTotalSize()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            recorder.AddFile(new FileRecord("file1.txt", 100));
            recorder.AddFile(new FileRecord("file2.txt", 200));

            Assert.Equal(300, recorder.GetTotalSize());
        }

        [Fact]
        public void GetTopNCollections_ShouldReturnCorrectTopCollections()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            recorder.AddFile(new FileRecord("file1.txt", 100, new List<string> { "collection1" }));
            recorder.AddFile(new FileRecord("file2.txt", 200, new List<string> { "collection2" }));
            recorder.AddFile(new FileRecord("file3.txt", 300, new List<string> { "collection2" }));

            var topCollections = recorder.GetTopNCollections(2);

            Assert.Equal(new List<string> { "collection2-500", "collection1-100" }, topCollections);
        }

        [Fact]
        public void AddFile_ShouldUpdateParentCollectionSize()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            recorder.AddCollectionHierarchy("collection2", "collection1");
            recorder.AddFile(new FileRecord("file1.txt", 200, new List<string> { "collection2" }));

            var topCollections = recorder.GetTopNCollections(1);
            Assert.Equal(new List<string> { "collection1-200" }, topCollections);
        }

        [Fact]
        public void AddFile_WithMultipleCollections_ShouldUpdateAllCollections()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            recorder.AddFile(new FileRecord("file1.txt", 200, new List<string> { "collection1", "collection2" }));

            var topCollections = recorder.GetTopNCollections(2);
            Assert.Contains("collection1-200", topCollections);
            Assert.Contains("collection2-200", topCollections);
        }

        [Fact]
        public void GetTopNCollections_WithNoCollections_ShouldReturnEmptyList()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            var topCollections = recorder.GetTopNCollections(3);

            Assert.Empty(topCollections);
        }

        [Fact]
        public void GetTotalSize_WithNoFiles_ShouldReturnZero()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            Assert.Equal(0, recorder.GetTotalSize());
        }

        [Fact]
        public void GetTopNCollections_WhenNIsGreaterThanAvailableCollections_ShouldReturnAll()
        {
            var recorder = new FileRecorderCode.FileRecorder();
            recorder.AddFile(new FileRecord("file1.txt", 100, new List<string> { "collection1" }));

            var topCollections = recorder.GetTopNCollections(5);
            Assert.Equal(new List<string> { "collection1-100" }, topCollections);
        }
}