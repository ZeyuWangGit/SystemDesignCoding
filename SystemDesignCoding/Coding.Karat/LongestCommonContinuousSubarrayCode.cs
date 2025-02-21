namespace Coding.Karat;

public static class LongestCommonContinuousSubarrayCode
{
    /*
    Longest Common Continuous Subarray
    [
      ["3234.html", "xys.html", "7hsaa.html"],
      ["3234.html", "sdhsfjdsh.html", "xys.html", "7hsaa.html"]
    ]
    => ["xys.html", "7hsaa.html"]

        Write a function that takes two users' browsing histories as input and
       - returns the longest contiguous sequence of URLs that appears in both.
       - Sample input:
       - user0 = ["/start", "/green", "/blue", "/pink", "/register", "/orange",
       - "/one/two"] user1 = ["/start", "/pink", "/register", "/orange", "/red", "a"]
       - user2 = ["a", "/one", "/two"] user3 = ["/pink", "/orange", "/yellow",
       - "/plum", "/blue", "/tan", "/red", "/amber", "/HotRodPink", "/CornflowerBlue",
       - "/LightGoldenRodYellow", "/BritishRacingGreen"] user4 = ["/pink", "/orange",
       - "/amber", "/BritishRacingGreen", "/plum", "/blue", "/tan", "/red",
       - "/lavender", "/HotRodPink", "/CornflowerBlue", "/LightGoldenRodYellow"] user5
       - = ["a"] user6 = ["/pink","/orange","/six","/plum","/seven","/tan","/red",
       - "/amber"]
       - Sample output:
       - findContiguousHistory(user0, user1) => ["/pink", "/register", "/orange"]
       - findContiguousHistory(user0, user2) => [] (empty)
       - findContiguousHistory(user0, user0) => ["/start", "/green", "/blue", "/pink",
       - "/register", "/orange", "/one/two"] findContiguousHistory(user2, user1) =>
       - ["a"] findContiguousHistory(user5, user2) => ["a"]
       - findContiguousHistory(user3, user4) => ["/plum", "/blue", "/tan", "/red"]
       - findContiguousHistory(user4, user3) => ["/plum", "/blue", "/tan", "/red"]
       - findContiguousHistory(user3, user6) => ["/tan", "/red", "/amber"]
       - n: length of the first user's browsing history m: length of the second user's
       - browsing history

       * /
     */
    public static List<string> LongestCommonContinuousSubarray(List<string> history1, List<string> history2)
    {
        var n = history1.Count;
        var m = history2.Count;
        var dp = new int[n, m];
        var maxLength = 0;
        var endIndex = 0;

        for (var i = 0; i < n; i++)
        {
            dp[i, 0] = history1[i] == history2[0] ? 1 : 0;
        }

        for (var j = 0; j < m; j++)
        {
            dp[0, j] = history1[0] == history2[j] ? 1 : 0;
        }

        for (var i = 1; i < n; i++)
        {
            for (var j = 1; j < m; j++)
            {
                if (history1[i] == history2[j])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                    if (dp[i, j] > maxLength)
                    {
                        maxLength = dp[i, j];
                        endIndex = i;
                    }
                }
                else
                {
                    dp[i, j] = 0;
                }
            }
        }

        return maxLength > 0 ? history1.GetRange(endIndex - maxLength + 1, maxLength) : [];
    }
}