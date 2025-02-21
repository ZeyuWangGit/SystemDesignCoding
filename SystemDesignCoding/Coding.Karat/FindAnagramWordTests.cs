namespace Coding.Karat;

public class FindAnagramWordTests
    {
        [Fact]
        public void Test_AnagramExists_ReturnsCorrectWord()
        {
            var words = new List<string> { "cat", "baby", "bird", "fruit" };
            string inputString = "tacjbcebef";
            string expected = "cat"; // "tac" is an anagram of "cat"

            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_MultipleMatches_ReturnsFirstMatch()
        {
            var words = new List<string> { "baby", "bird", "fruit" };
            string inputString = "bacdrigb";
            string expected = "bird"; // "drib" is an anagram of "bird"

            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_NoMatchingAnagram_ReturnsNull()
        {
            var words = new List<string> { "apple", "grape", "orange" };
            string inputString = "xyzabc";
            
            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Null(result);
        }

        [Fact]
        public void Test_EmptyWordList_ReturnsNull()
        {
            var words = new List<string>();
            string inputString = "abcdef";

            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Null(result);
        }

        [Fact]
        public void Test_EmptyString_ReturnsNull()
        {
            var words = new List<string> { "cat", "dog", "apple" };
            string inputString = "";

            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Null(result);
        }

        [Fact]
        public void Test_LongStringWithAnagram_ReturnsCorrectWord()
        {
            var words = new List<string> { "hello", "world", "python" };
            string inputString = "lloehworldpy";

            string expected = "hello"; // "lloeh" is an anagram of "hello"

            string result = FindAnagramWord.FindMatchingAnagram(words, inputString);
            Assert.Equal(expected, result);
        }
    }