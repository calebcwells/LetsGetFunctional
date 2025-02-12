namespace LetsGetFunctional.Poker;

public class Card(CardValue value, CardSuit suit)
{
    public CardValue Value { get; } = value;
    public CardSuit Suit { get; } = suit;
    
    public override string ToString() => $"{Value} of {Suit}";
}