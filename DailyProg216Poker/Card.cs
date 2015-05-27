using System;
using System.Linq;

namespace DailyProg216Poker
{
	public class Card
	{
		public Suit suit { get; set; }
		public Value value { get; set; }

		public Card (int value, int suit)
		{
			this.suit = (Suit)suit;
			this.value = (Value)value;
		}

		public string getName()
		{
			return this.value + " of " + this.suit.ToString ().Substring (0, 1) + this.suit.ToString ().Substring (1).ToLower ();
		}

		public string getShortName()
		{
			Value[] v = { Value.Ace, Value.Jack, Value.Queen, Value.King };
			string s = "";
			if (v.Contains (this.value))
			{
				s = this.value.ToString ().Substring (0, 1) + this.suit.ToString ().Substring (0, 1);
			}
			else {
				s = (int)this.value + this.suit.ToString ().Substring (0, 1);
			}
				
			return s;
		}
	}
}

