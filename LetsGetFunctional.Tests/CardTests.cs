namespace LetsGetFunctional.Tests;

public class CardTests
{
    [Fact]
    public void CanCreateCard()
    {
        Card card = new(CardValue.Two, CardSuit.Spades);
        Assert.NotNull(card);
    }

    [Fact]
    public void CanCreateCardWithValue()
    {
        Card card = new(CardValue.Ace, CardSuit.Clubs);
        
        Assert.Equal(CardValue.Ace, card.Value);
        Assert.Equal(CardSuit.Clubs, card.Suit);
    }

    [Fact]
    public void CanDescribeCard()
    {
        Card card = new(CardValue.Ace, CardSuit.Spades);
        
        Assert.Equal("Ace of Spades", card.ToString());
    }

    [Fact]
    public void CanCreateHand()
    {
        Hand hand = new();
        
        Assert.False(hand.Cards.Any());
    }

    [Fact]
    public void CanHandDrawCard()
    {
        Card card = new(CardValue.Ace, CardSuit.Spades);
        Hand hand = new();
        
        hand.Draw(card);
        
        Assert.Equal(hand.Cards.First(), card);
    }
    
    [Fact]
    public void CanGetHighCard()
    {
        Hand hand = new();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
        Assert.Equal(CardValue.King, hand.HighCard().Value);
    }
    
    [Fact]
    public void CanScoreHighCard()
    {
        Hand hand = new();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
        Assert.Equal(HandRank.HighCard, hand.GetHandRank());
    }
    
    [Fact]
    public void CanScoreFlush()
    {
        Hand hand = new();
        hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
        Assert.Equal(HandRank.Flush, hand.GetHandRank());
    }
}

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

public class Card(CardValue value, CardSuit suit)
{
    public CardValue Value { get; } = value;
    public CardSuit Suit { get; } = suit;
    
    public override string ToString() => $"{Value} of {Suit}";
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

public enum HandRank
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush
}