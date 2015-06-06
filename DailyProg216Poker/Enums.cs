using System;

namespace DailyProg216Poker
{
	public static class References
	{
		// This assumes the structure of the Value Enum
		public static string GetValueString(int value)
		{
			switch(value)
			{
			case 1:
				return "Ace";
			case 2:
				return "Two";
			case 3:
				return "Three";
			case 4:
				return "Four";
			case 5:
				return "Five";
			case 6:
				return "Six";
			case 7:
				return "Seven";
			case 8:
				return "Eight";
			case 9:
				return "Nine";
			case 10:
				return "Ten";
			case 11:
				return "Jack";
			case 12:
				return "Queen";
			case 13:
				return "King";
			}
			return "";
		}

		public static string GetPluralString(int value)
		{
			switch(value)
			{
			case 1:
				return "Aces";
			case 2:
				return "Twos";
			case 3:
				return "Threes";
			case 4:
				return "Fours";
			case 5:
				return "Fives";
			case 6:
				return "Sixes";
			case 7:
				return "Sevens";
			case 8:
				return "Eights";
			case 9:
				return "Nines";
			case 10:
				return "Tens";
			case 11:
				return "Jacks";
			case 12:
				return "Queens";
			case 13:
				return "Kings";
			}
			return "";
		}
	}

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

