namespace LetsGetFunctional.Tests;

public class CardTests
{
    [Fact]
    public void CanCreateCard()
    {
        Card card = new Card(CardValue.Two, CardSuit.Spades);
        Assert.NotNull(card);
    }

    [Fact]
    public void CanCreateCardWithValue()
    {
        Card card = new Card(CardValue.Ace, CardSuit.Clubs);
        
        Assert.Equal(CardValue.Ace, card.Value);
        Assert.Equal(CardSuit.Clubs, card.Suit);
    }
}

public class Card
{
    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
    public CardValue Value { get; set; }
    public CardSuit Suit { get; set; }
}

public enum CardSuit
{
    Spades,
    Diamonds,
    Clubs,
    Hearts
}

public enum CardValue
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}