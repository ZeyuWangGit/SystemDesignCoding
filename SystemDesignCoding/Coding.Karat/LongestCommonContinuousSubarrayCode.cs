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