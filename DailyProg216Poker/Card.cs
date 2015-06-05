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
			return value + " of " + suit.ToString ().Substring (0, 1) + suit.ToString ().Substring (1).ToLower ();
		}

		public string getShortName()
		{
			char[] symbols = { '\u2665', '\u2666', '\u2660', '\u2663' };
			Value[] v = { Value.Ace, Value.Jack, Value.Queen, Value.King };
			string s = "";
			if (v.Contains (value))
			{
				s = value.ToString ().Substring (0, 1);
			}
			else {
				s = (int)value + "";
			}
			s += symbols[(int)suit];
				
			return s;
		}
	}
}

