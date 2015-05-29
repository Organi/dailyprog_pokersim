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
			char[] symbols = { '♥', '♦', '♠', '♣' };
			Value[] v = { Value.Ace, Value.Jack, Value.Queen, Value.King };
			string s = "";
			if (v.Contains (this.value))
			{
				s = this.value.ToString ().Substring (0, 1);
			}
			else {
				s = (int)value + "";
			}
			s += symbols[(int)this.suit];
				
			return s;
		}
	}
}

