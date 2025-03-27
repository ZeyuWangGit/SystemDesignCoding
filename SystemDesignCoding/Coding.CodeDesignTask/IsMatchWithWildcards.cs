namespace Coding.CodeDesignTask;

public static class IsMatchWithWildcardsCode
{
    public static bool IsMatchWithWildcards(string word, string abbr)
    {
        var index = 0;
        foreach (var c in abbr)
        {
            if (char.IsDigit(c))
            {
                index += c - '0';
                if (index > word.Length)
                {
                    return false;
                }
            }
            else
            {
                if (index >= word.Length || word[index] != c)
                {
                    return false;
                }

                index++;
            }
        }

        return index == word.Length;
    }
}