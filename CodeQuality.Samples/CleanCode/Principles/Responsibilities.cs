namespace CodeQuality.Samples.CleanCode.Principles;

public class NotificationService
{
    public List<string> Logs { get; set; } = new();
    public List<Customer> Users { get; set; } = new();

    public void ExportLogsToCsv()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "logs.csv");
        File.WriteAllLines(path, Logs);
        Console.WriteLine($"Logs exported to {path}");
    }

    public void RegisterUser(string name, string email, string phone, string preference)
    {
        var user = new Customer
        {
            Name = name,
            Email = email,
            Phone = phone,
            ContactPreference = preference,
            CreatedAt = DateTime.Now
        };

        Users.Add(user);
        Logs.Add($"[{DateTime.Now}] User registered: {name}");
    }

    public void SendMessage(string userName, string message)
    {
        var user = Users.FirstOrDefault(u => u.Name == userName);
        if (user == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        if (user.ContactPreference == "Email")
        {
            var smtp = new SmtpClient();
            smtp.Send("noreply@company.com", user.Email, "Notification", message);
            Logs.Add($"[{DateTime.Now}] Email sent to {user.Email}");
        }
        else if (user.ContactPreference == "Sms")
        {
            var gateway = new SmsGateway();
            gateway.SendSms(user.Phone, message);
            Logs.Add($"[{DateTime.Now}] SMS sent to {user.Phone}");
        }
        else
        {
            Console.WriteLine("Unknown contact preference");
        }
    }
}

public class Customer
{
    public string ContactPreference { get; set; }
    public DateTime CreatedAt{ get; set; }
    public string Email{ get; set; }
    public string Name{ get; set; }
    public string Phone{ get; set; }
}

public class SmtpClient
{
    public void Send(string from, string to, string subject, string body) => Console.WriteLine($"SMTP {subject} to {to}");
}

public class SmsGateway
{
    public void SendSms(string phone, string message) => Console.WriteLine($"SMS → {phone}: {message}");
}