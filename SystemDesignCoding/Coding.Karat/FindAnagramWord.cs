namespace Coding.Karat;

public static class FindAnagramWord
{
    /*
    Given a list of words and a string, find the word whose anagram is present in the given string.
    Example:
    Words: ["cat", "baby", "bird", "fruit"]
    String1: "tacjbcebef"
    Answer: "cat"
    String2: "bacdrigb"
    Answer: "bird"
    */
    public static string FindMatchingAnagram(List<string> words, string inputString)
    {
        var characters = Encode(inputString);

        foreach (var word in words)
        {
            if (IsAnagram(characters, Encode(word)))
            {
                return word;
            }
        }

        return null;
    }

    private static bool IsAnagram(int[] characters, int[] word)
    {
        for (int i = 0; i < 26; i++)
        {
            if (word[i] > 0)
            {
                if (characters[i] < word[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int[] Encode(string word)
    {
        var characters = new int[26];
        foreach (var c in word)
        {
            characters[c - 'a']++;
        }

        return characters;
    }
}