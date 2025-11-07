namespace CodeQuality.Samples.CleanCode.Principles;

public class Naming
{
    private UserManager m = new();
    private readonly bool flag = true;
    public bool Process(IEnumerable<User> u)
    {
        if (flag)
        {
            foreach (var a in u)
            {
                m.DoSomething(a);
            }
        }

        return flag;
    }
}

public record User()
{
}

public class UserManager
{
    public void DoSomething(User u)
    {
        
    }
}