namespace Coding.Karat;

using System;
using System.Collections.Generic;
using Xunit;

public class AdConversionRateTests
{
    [Fact]
    public void TestAdConversionRate()
    {
        string[] completedPurchaseUserIds = { "3123122444", "234111110", "8321125440", "99911063" };
        string[] adClicks =
        {
            "122.121.0.1,2016-11-03 11:41:19,Buy wool coats for your pets",
            "96.3.199.11,2016-10-15 20:18:31,2017 Pet Mittens",
            "122.121.0.250,2016-11-01 06:13:13,The Best Hollywood Coats",
            "82.1.106.8,2016-11-12 23:05:14,Buy wool coats for your pets",
            "92.130.6.144,2017-01-01 03:18:55,Buy wool coats for your pets",
            "92.130.6.145,2017-01-01 03:18:55,2017 Pet Mittens"
        };
        string[] allUserIps =
        {
            "2339985511,122.121.0.155",
            "234111110,122.121.0.1",
            "3123122444,92.130.6.145",
            "39471289472,2001:0db8:ac10:fe01:0000:0000:0000:0000",
            "8321125440,82.1.106.8",
            "99911063,92.130.6.144"
        };

        var expected = new List<string>
        {
            "3 of 3 Buy wool coats for your pets", 
            "1 of 2 2017 Pet Mittens", 
            "0 of 1 The Best Hollywood Coats"
        };

        var result = AdConversionRateCode.AdConversionRate(completedPurchaseUserIds, adClicks, allUserIps);
        Assert.Equal(expected, result);
    }
}