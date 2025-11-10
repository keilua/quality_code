namespace CodeQuality.Samples.CleanCode.Principles;

public class Functions
{
    public async Task ProcessOrders(List<Order> orders)
    {
        Console.WriteLine("Processing orders...");

        foreach (var order in orders)
        {
            if (order == null)
            {
                Console.WriteLine("Invalid order detected, skipping...");
                continue;
            }

            if (string.IsNullOrEmpty(order.CustomerEmail))
            {
                Console.WriteLine($"Order {order.Id} skipped: no customer email.");
                continue;
            }

            if (order.TotalAmount <= 0)
            {
                Console.WriteLine($"Order {order.Id} skipped: invalid total amount.");
                continue;
            }

            await OrderService.SendEmail(order.Id, order.CustomerEmail);
            Console.WriteLine($"Order {order.Id} processed successfully.");
        }

        Console.WriteLine("All orders processed.");
    }
}

public record Order(int Id, string CustomerEmail, decimal TotalAmount);

public class OrderService
{
    public static async Task SendEmail(int id, string customerEmail)
    {
        Console.WriteLine($"Sending confirmation email to {customerEmail} for order {id}...");
        await Task.Delay(200);
    }
}