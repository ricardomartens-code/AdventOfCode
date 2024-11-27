namespace Day_7
{
    public class Hand
    {
        public string? Cards { get; set; }
        public string? OriginalCards { get; set; }
        public PokerType HandType { get; set; }
        public int BidAmount { get; set; }
        public long Score { get; set; }
    }
}
