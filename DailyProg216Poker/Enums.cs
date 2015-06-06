using System;

namespace DailyProg216Poker
{
	public enum Decision { FOLD, BET, CHECK, RAISE };
	public enum Suit { HEARTS, DIAMONDS, SPADES, CLUBS };
	public enum Value {
		Ace = 1,
		Two = 2, 
		Three = 3, 
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9,
		Ten = 10,
		Jack = 11,
		Queen = 12,
		King = 13
	};
	public enum Plurals {
		Aces = 1,
		Twos = 2,
		Threes = 3,
		Fours = 4,
		Fives = 5,
		Sixes = 6,
		Sevens = 7,
		Eights = 8,
		Nines = 9,
		Tens = 10,
		Jacks = 11,
		Queens = 12,
		Kings = 13
	}
	public enum HandType {
		STRAIGHT_FLUSH,
		FOUR_OF_A_KIND,
		FULL_HOUSE,
		FLUSH,
		STRAIGHT,
		THREE_OF_A_KIND,
		TWO_PAIR,
		PAIR,
		HIGH_CARD
	}
}

