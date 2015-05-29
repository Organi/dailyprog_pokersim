﻿using System;

namespace DailyProg216Poker
{
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
	public enum Decision { FOLD, BET, CHECK, RAISE };
}

