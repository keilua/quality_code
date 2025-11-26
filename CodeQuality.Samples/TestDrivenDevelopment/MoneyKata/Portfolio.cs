using FluentAssertions;
using Xunit;

namespace CodeQuality.Samples.TestDrivenDevelopment.MoneyKata;

/// <summary>
/// Simulate a stock portfolio.
/// You can add amounts in your portfolio (ex: 10 EUR, 15 USD). The portfolio will hold several "amounts" in different currencies.
/// We want to evaluate the portfolio in a specific currency: what is the EUR value of my portfolio?
///
/// Here's an exchange rate table:
/// FROM    | TO    | RATE
/// EUR     USD     1.2
/// USD     EUR     0.82
/// USD     KRW     1100
/// KRW     EUR     0.0009
/// EUR     KRW     1344
/// </summary>
public class Portfolio
{
    public decimal Evaluate() => 0;
}

public class PortfolioTest
{
    [Fact]
    public void EmptyPortfolio_ShouldReturnZero()
    {
        var portfolio = new Portfolio();
        var amount = portfolio.Evaluate();
        amount.Should().Be(0);
    }
}