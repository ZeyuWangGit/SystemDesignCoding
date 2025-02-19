namespace Coding.Karat;

public class WordWrapTests
{
    [Fact]
    public void Test_WordWrap_ExampleCase()
    {
        string[] words = { "apple", "banana", "cherry", "date" };
        int maxLen = 10;

        List<string> expected = new List<string>
        {
            "apple",
            "banana",
            "cherry",
            "date"
        };

        var result = WordWrapCode.WordWrap(words, maxLen);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WordWrap_EmptyInput()
    {
        string[] words = { };
        int maxLen = 10;

        List<string> expected = new List<string>();

        var result = WordWrapCode.WordWrap(words, maxLen);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WordWrap_MultipleWordsFitExactly()
    {
        string[] words = { "a", "bb", "ccc", "dddd", "eeeee", "ffffff" };
        int maxLen = 10;

        List<string> expected = new List<string>
        {
            "a-bb-ccc",
            "dddd-eeeee",
            "ffffff"
        };

        var result = WordWrapCode.WordWrap(words, maxLen);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WordWrap_LongWordsMustBeAlone()
    {
        string[] words = { "hello", "world", "test" };
        int maxLen = 10;

        List<string> expected = new List<string>
        {
            "hello",
            "world-test",
        };

        var result = WordWrapCode.WordWrap(words, maxLen);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WordWrap_LargeCase()
    {
        string[] words = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
        int maxLen = 15;

        List<string> expected = new List<string>
        {
            "The-quick-brown",
            "fox-jumps-over",
            "the-lazy-dog"
        };

        var result = WordWrapCode.WordWrap(words, maxLen);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Test_ReflowAndJustify_ExampleCase()
    {
        string[] lines = {
            "The day began as still as the",
            "night abruptly lighted with",
            "brilliant flame"
        };
        int maxWidth = 24;

        List<string> expected = new List<string>
        {
            "The--day--began-as-still",
            "as--the--night--abruptly",
            "lighted--with--brilliant",
            "flame"
        };

        var result = WordWrapCode.ReflowAndJustify(lines, maxWidth);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ReflowAndJustify_SingleShortLine()
    {
        string[] lines = { "Hello world" };
        int maxWidth = 20;

        List<string> expected = new List<string>
        {
            "Hello----------world"
        };

        var result = WordWrapCode.ReflowAndJustify(lines, maxWidth);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ReflowAndJustify_SingleLongWord()
    {
        string[] lines = { "Supercalifragilisticexpialidocious" };
        int maxWidth = 34;

        List<string> expected = new List<string>
        {
            "Supercalifragilisticexpialidocious"
        };

        var result = WordWrapCode.ReflowAndJustify(lines, maxWidth);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ReflowAndJustify_MultipleLines()
    {
        string[] lines = {
            "Lorem ipsum dolor sit amet,",
            "consectetur adipiscing elit,",
            "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
        };
        int maxWidth = 30;

        List<string> expected = new List<string>
        {
            "Lorem--ipsum--dolor--sit-amet,",
            "consectetur--adipiscing--elit,",
            "sed----do----eiusmod----tempor",
            "incididunt-ut-labore-et-dolore",
            "magna------------------aliqua."
        };

        var result = WordWrapCode.ReflowAndJustify(lines, maxWidth);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ReflowAndJustify_EmptyInput()
    {
        string[] lines = { };
        int maxWidth = 10;

        List<string> expected = new List<string>();

        var result = WordWrapCode.ReflowAndJustify(lines, maxWidth);
        Assert.Equal(expected, result);
    }
}