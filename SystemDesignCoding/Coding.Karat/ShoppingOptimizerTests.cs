namespace Coding.Karat;

public class ShoppingOptimizerTests
{
    private readonly List<(string productName, string department)> products = new()
    {
        ("Cheese", "Dairy"),
        ("Carrots", "Produce"),
        ("Potatoes", "Produce"),
        ("Canned Tuna", "Pantry"),
        ("Romaine Lettuce", "Produce"),
        ("Chocolate Milk", "Dairy"),
        ("Flour", "Pantry"),
        ("Iceberg Lettuce", "Produce"),
        ("Coffee", "Pantry"),
        ("Pasta", "Pantry"),
        ("Milk", "Dairy"),
        ("Blueberries", "Produce"),
        ("Pasta Sauce", "Pantry")
    };

    [Fact]
    public void Test_List1()
    {
        var shoppingList = new List<string> { "Blueberries", "Milk", "Coffee", "Flour", "Cheese", "Carrots" };
        int result = ShoppingOptimizer.CalculateDepartmentVisitSavings(products, shoppingList);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_List2()
    {
        var shoppingList = new List<string> { "Blueberries", "Carrots", "Coffee", "Milk", "Flour", "Cheese" };
        int result = ShoppingOptimizer.CalculateDepartmentVisitSavings(products, shoppingList);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_List3()
    {
        var shoppingList = new List<string> { "Blueberries", "Carrots", "Romaine Lettuce", "Iceberg Lettuce" };
        int result = ShoppingOptimizer.CalculateDepartmentVisitSavings(products, shoppingList);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_List4()
    {
        var shoppingList = new List<string> { "Milk", "Flour", "Chocolate Milk", "Pasta Sauce" };
        int result = ShoppingOptimizer.CalculateDepartmentVisitSavings(products, shoppingList);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_List5()
    {
        var shoppingList = new List<string> { "Cheese", "Potatoes", "Blueberries", "Canned Tuna" };
        int result = ShoppingOptimizer.CalculateDepartmentVisitSavings(products, shoppingList);
        Assert.Equal(0, result);
    }
} 
