using System.Text;

namespace Coding.Karat;

public static class WordWrapCode
{
    /*
    给一个word list 和最大的长度，要求把这些word用 - 串联起来，但不能超过最大的长度。
     */
    public static List<string> WordWrap(string[] words, int maxLen)
    {
        var res = new List<string>();
        if (words.Length == 0 || maxLen == 0)
        {
            return res;
        }

        var i = 0;
        while (i < words.Length)
        {
            var remain = maxLen;
            var count = 0;
            while (i < words.Length)
            {
                if (remain - words[i].Length < 0)
                {
                    break;
                }

                count++;
                remain -= words[i++].Length + 1;
            }

            var arr = new List<string>();
            for (var k = 0; k < count; k++)
            {
                arr.Add(words[i - count + k]);
            }
            res.Add(string.Join("-", arr.ToArray()));
        }

        return res;
    }
    
    /*
    We are building a word processor, and we would like to implement a "reflow" functionality that also applies full justification to the text.
    Given an array containing lines of text and a new maximum width, re-flow the text to fit the new width.
    Each line should have the exact specified width. If any line is too short, insert '-' (as stand-ins for spaces) 
    between words as equally possible until it fits.
    Note: we are using '-' instead of spaces between words to make testing and visual verification of the results easier.

    lines = [ "The day began as still as the",
          "night abruptly lighted with",
          "brilliant flame" ]

    reflowAndJustify(lines, 24) ... "reflow lines and justify to length 24" =>

        [ "The--day--began-as-still",
          "as--the--night--abruptly",
          "lighted--with--brilliant",
          "flame" ] // <--- a single word on a line is not padded with spaces
     */
    public static List<String> ReflowAndJustify(String[] lines, int maxWidth)
    {
        var words = SplitWords(lines, maxWidth);
        return words.Select(word => JustifyWord(word, maxWidth)).ToList();
    }

    private static List<List<string>> SplitWords(string[] lines, int maxLen)
    {
        var words = lines.SelectMany(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();
        var res = new List<List<string>>();
        int i = 0;
        while (i < words.Count)
        {
            int remain = maxLen;
            int count = 0;
            while (i < words.Count)
            {
                if (remain - words[i].Length < 0)
                {
                    break;
                }
                count++;
                remain -= words[i++].Length + 1;
            }

            var arr = new List<string>();
            for (int k = 0; k < count; k++)
            {
                arr.Add(words[i - count + k]);
            }
            res.Add(arr);

        }

        return res;
    }

    private static string JustifyWord(List<string> words, int maxLen)
    {
        int wordCount = words.Count;
        if (wordCount == 1)
        {
            return words[0];
        }
        int gapCount = wordCount - 1;
        int remain = maxLen - words.Sum(word => word.Length);
        int baseCount = remain / gapCount;
        int extraCount = remain % gapCount;
        var sb = new StringBuilder();
        for (int i = 0; i < wordCount - 1; i++)
        {
            sb.Append(words[i]);
            for (int k = 0; k < baseCount; k++)
            {
                sb.Append('-');
            }

            if (i < extraCount)
            {
                sb.Append('-');
            }
        }

        sb.Append(words[^1]);
        return sb.ToString();
    }
}