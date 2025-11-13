#region

using FluentAssertions;

#endregion

namespace CodeQuality.Samples.CleanCode.Principles;

public class Cart
{
    public int ItemsCount { get; set; }

    public List<Item> Items { get; set; } = new();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public float CalculateTotal()
    {
        return Items.Sum(item => item.Price);
    }

    public List<Item> GetItems()
    {
        return Items;
    }
}

public class Item(string name, float price)
{
    public string Name { get; set; } = name;
    public float Price { get; set; } = price;
}

public class Tests
{
    [Fact]
    public void CartCalculationTest()
    {
        var cart = new Cart();
        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
        {
            cart.AddItem(new Item("Book", 10));
        }
        else
        {
            cart.AddItem(new Item("Pen", 2));
            cart.AddItem(new Item("Notebook", 5));
        }

        var total = cart.CalculateTotal();
        if (DateTime.Now.DayOfWeek != DayOfWeek.Monday)
            total.Should().Be(7);
        else
            total.Should().Be(10);

        if (cart.ItemsCount > 1)
        {
            var firstItem = cart.GetItems().First();
            firstItem.Price.Should().BeGreaterThan(0);
        }
    }
}