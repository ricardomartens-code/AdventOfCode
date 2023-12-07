using Day_7;
using Utilities;

internal class Program
{
    static void Main(string[] args)
    {
        string[] lines = AoCUtilities.GetInput();
        List<Hand> hands = new();

        Dictionary<char, int> cardWeights = new()
        {
                { 'A', 140 }, {'K', 130}, {'Q', 120}, {'T', 110},
                {'9', 100}, {'8', 90}, {'7', 80}, {'6', 70}, {'5', 60},
                {'4', 50}, {'3', 40}, {'2', 30}, {'J', 20} 
            };

        foreach (string line in lines)
        {
            int index = line.IndexOf(" ");
            int bidAmount = Convert.ToInt32(line.Substring(index + 1));
            string handCards = line.Substring(0, index);

            char[] cards = handCards.OrderByDescending(c => cardWeights[c]).ToArray();

            var counts = cards.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            Hand hand = new()
            {
                Cards = new string(handCards),
                OriginalCards = handCards,
                HandType = DetermineHandType(counts),
                BidAmount = bidAmount
            };

            hands.Add(hand);
        }

        hands.Sort(CompareHands);

        int totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            int rank = hands.Count - i;
            Console.WriteLine($"Rank {rank}: {hands[i].OriginalCards}");
            int handWinnings = rank * hands[i].BidAmount;
            totalWinnings += handWinnings;
        }
        Console.WriteLine($"Total winnings: {totalWinnings}");

    }

    public static PokerType DetermineHandType(Dictionary<char, int> counts)
    {
        var effectiveCounts = GetEffectiveJokerCounts(counts);
        if (effectiveCounts.Values.Count == 0)
            return PokerType.FiveOfAKind;
        int maxCount = effectiveCounts.Values.Max();

        if (maxCount == 5)
            return PokerType.FiveOfAKind;
        else if (maxCount == 4)
            return PokerType.FourOfAKind;
        else if (maxCount == 3 && effectiveCounts.Values.Contains(2))
            return PokerType.FullHouse;
        else if (maxCount == 3)
            return PokerType.ThreeOfAKind;
        else if (effectiveCounts.Values.Count(c => c == 2) == 2)
            return PokerType.TwoPair;
        else if (maxCount == 2)
            return PokerType.OnePair;
        else
            return PokerType.HighCard;
    }


    public static int CompareHands(Hand hand1, Hand hand2)
    {
        if (hand1.HandType > hand2.HandType)
            return -1;
        if (hand1.HandType < hand2.HandType)
            return 1;

        var hand1Cards = hand1.OriginalCards?.Select(c => GetCardValue(c)).ToList();
        var hand2Cards = hand2.OriginalCards?.Select(c => GetCardValue(c)).ToList();

        for (int i = 0; i < hand1Cards?.Count; i++)
        {
            if (hand1Cards[i] > hand2Cards?[i])
                return -1;
            if (hand1Cards[i] < hand2Cards?[i])
                return 1;
        }

        return 0;
    }


    public static int GetCardValue(char card)
    {
        switch (card)
        {
            case 'A':
                return 14;
            case 'K':
                return 13;
            case 'Q':
                return 12;
            case 'T':
                return 10;
            case 'J':
                return 1;
            default:
                return card - '0';
        }
    }

    public static Dictionary<char, int> GetEffectiveJokerCounts(Dictionary<char, int> counts)
    {
        int jokerCount = counts.ContainsKey('J') ? counts['J'] : 0;
        var nonJokerCounts = counts.Where(c => c.Key != 'J').ToDictionary(c => c.Key, c => c.Value);

        if (jokerCount > 0 && nonJokerCounts.Count > 0)
        {
            var maxCountCard = nonJokerCounts.OrderByDescending(c => c.Value).First().Key;
            nonJokerCounts[maxCountCard] += jokerCount;
        }

        return nonJokerCounts;
    }


}
