namespace CodeQuality.Samples.Quality;

public class Account
{
    public enum Currency
    {
    }

    public bool CanWithdraw(Money money) =>
        HasSufficientFunds(money.Amount)
        && IsWithinDailyLimit(money.Amount)
        && IsWithinWeeklyLimit(money.Amount)
        && IsTargetCurrencyAllowed(money.Currency);

    private bool HasSufficientFunds(decimal amount) => true;

    private bool IsTargetCurrencyAllowed(Currency targetCurrency) => true;

    private bool IsWithinDailyLimit(decimal amount) => true;

    private bool IsWithinWeeklyLimit(decimal amount) => true;

    public record Money(decimal Amount, Currency Currency);
}