namespace CodeQuality.Samples.Quality;

public class PhoneNumberVerification
{
    public static bool v(string t)
    {
        if (t == null)
            return false;
        if (t.Length < 8 || t.Length > 15)
            return false;
        if (!t.StartsWith("+"))
            return false;
        for (var i = 1; i < t.Length; i++)
            if (!int.TryParse(t[i].ToString(), out _))
                return false;

        return true;
    }
}