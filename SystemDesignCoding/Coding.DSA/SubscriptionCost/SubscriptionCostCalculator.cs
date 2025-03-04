namespace Coding.DSA.SubscriptionCost;
public enum SubscriptionType
{
    Basic,
    Standard,
    Premium
}

public static class SubscriptionPrices
{
    public static readonly Dictionary<SubscriptionType, decimal> Prices = new()
    {
        { SubscriptionType.Basic, 19.99m },
        { SubscriptionType.Standard, 30.99m },
        { SubscriptionType.Premium, 99.99m }
    };
}

public class SubscriptionCostCalculator
{
    public static decimal CalculateAnnualCost(SubscriptionType type, DateTime startDate)
    {
        if (!SubscriptionPrices.Prices.ContainsKey(type))
            throw new ArgumentException("Invalid subscription type.");

        decimal monthlyCost = SubscriptionPrices.Prices[type];
        int year = startDate.Year;
        decimal totalCost = 0;

        // 计算部分月份费用
        DateTime endOfMonth = new DateTime(year, startDate.Month, DateTime.DaysInMonth(year, startDate.Month));
        int remainingDays = (endOfMonth - startDate).Days + 1;
        totalCost += (monthlyCost / endOfMonth.Day) * remainingDays;

        // 计算完整月份费用
        for (int month = startDate.Month + 1; month <= 12; month++)
        {
            totalCost += monthlyCost;
        }

        return Math.Round(totalCost, 2);
    }
}