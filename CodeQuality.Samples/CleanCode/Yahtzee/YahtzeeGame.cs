namespace CodeQuality.Samples.CleanCode.Yahtzee;

/// <summary>
/// Objectif N°1: Refactorer le code selon les principes/pratiques de Clean Code
/// Objectif N°2: Exposer une seule méthode Evaluate(...) qui retournera la liste des figures possibles, avec leur score associé.
/// </summary>
public class YahtzeeGame
{
    protected int[] dice;

    public YahtzeeGame()
    {
    }

    public YahtzeeGame(int d1, int d2, int d3, int d4, int _5)
    {
        dice = new int[5];
        dice[0] = d1;
        dice[1] = d2;
        dice[2] = d3;
        dice[3] = d4;
        dice[4] = _5;
    }

    public static int Chance(int d1, int d2, int d3, int d4, int d5)
    {
        return d1+d2+d3+d4+d5;
    }   
    private int AggregateDiceValue(int diceValue)
{
    return dice.Where(d => d == diceValue).Sum();
}
    public int Sixes() => this.AggregateDiceValue(6);
    public int Fives() => this.AggregateDiceValue(5);
    public int Fours() => this.AggregateDiceValue(4);
    public int Threes()=> this.AggregateDiceValue(3);
    public int Twos() => this.AggregateDiceValue(2);
    public int Ones() => this.AggregateDiceValue(1);

    private static int OfAKind(int countRequired, params int[] dice)
    {
        int[] counts = new int[6];
        foreach (var die in dice)
            counts[die - 1]++;

        for (int i = 0; i < 6; i++)
            if (counts[i] >= countRequired)
                return (i + 1) * countRequired;

        return 0;
    }

    public static int ThreeOfAKind(int d1, int d2, int d3, int d4, int d5)
    {
        return OfAKind(3, d1, d2, d3, d4, d5);
    }

    public static int FourOfAKind(int d1, int d2, int d3, int d4, int d5)
    {
        return OfAKind(4, d1, d2, d3, d4, d5);
    }

    public static int FullHouse(int d1, int d2, int d3, int d4, int d5)
    {
        int[] counts = new int[6];
        foreach (var die in new[] { d1, d2, d3, d4, d5 })
            counts[die - 1]++;

        int pair = Array.FindIndex(counts, c => c == 2) + 1;
        int three = Array.FindIndex(counts, c => c == 3) + 1;

        return (pair > 0 && three > 0) ? pair * 2 + three * 3 : 0;
    }

    public static int LargeStraight(int d1, int d2, int d3, int d4, int d5)
    {
        int[] dice = { d1, d2, d3, d4, d5 };
        return Enumerable.Range(2, 5).All(n => dice.Contains(n)) ? 20 : 0;
    }
    public static int SmallStraight(int d1, int d2, int d3, int d4, int d5)
    {
        int[] dice = { d1, d2, d3, d4, d5 };
        return Enumerable.Range(1, 5).All(n => dice.Contains(n)) ? 15 : 0;
    }

    public int ScorePair(int d1, int d2, int d3, int d4, int d5)
    {
        var counts = new int[6];
        counts[d1 - 1]++;
        counts[d2 - 1]++;
        counts[d3 - 1]++;
        counts[d4 - 1]++;
        counts[d5 - 1]++;
        int at;
        for (at = 0; at != 6; at++)
            if (counts[6 - at - 1] >= 2)
                return (6 - at) * 2;
        return 0;
    }

    public static int TwoPair(int d1, int d2, int d3, int d4, int d5)
    {
        var counts = new int[6];
        counts[d1 - 1]++;
        counts[d2 - 1]++;
        counts[d3 - 1]++;
        counts[d4 - 1]++;
        counts[d5 - 1]++;
        var n = 0;
        var score = 0;
        for (var i = 0; i < 6; i += 1)
            if (counts[6 - i - 1] >= 2)
            {
                n++;
                score += 6 - i;
            }

        if (n == 2)
            return score * 2;
        return 0;
    }

    public static int Yahtzee(params int[] dice)
    {
        int score = OfAKind(5, dice);
        return score > 0 ? 50 : 0;
    }
}