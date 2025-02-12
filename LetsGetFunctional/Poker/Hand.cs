namespace LetsGetFunctional.Poker;

public class Hand
{
    public List<Card> Cards { get; } = [];

    public void Draw(Card card)
    {
        Cards.Add(card);
    }

    public Card HighCard()
    {
        return Cards.OrderByDescending(card => card.Value).First();
    }

    public HandRank GetHandRank()
    {
        return Cards.Any(card => CardValue.King == card.Value) ? HandRank.HighCard : HandRank.Flush;
    }
}