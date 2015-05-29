using System;

namespace DailyProg216Poker
{
	public class HPlayer : IPlayer
	{
		public int chips { get; set; }
		public Hand hand { get; set; }
		public string name { get; set; }

		public HPlayer (string n)
		{
			this.name = n;
		}

		public Tuple<Decision, int> GetDecision()
		{
			return Tuple.Create (Decision.CHECK, 0);
		}
	}
}

