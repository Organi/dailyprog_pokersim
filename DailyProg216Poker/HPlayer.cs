using System;

namespace DailyProg216Poker
{
	public class HPlayer : APlayer, IPlayer
	{
		public override int chips { get; set; }
		public override Hand hand { get; set; }
		public override string name { get; set; }

		public HPlayer (string n)
		{
			this.name = n;
		}

		public override Tuple<Decision, int> GetDecision()
		{
			return Tuple.Create (Decision.CHECK, 0);
		}
	}
}

