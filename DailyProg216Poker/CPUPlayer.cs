﻿using System;

namespace DailyProg216Poker
{
	public class CPUPlayer : APlayer, IPlayer
	{
		public override int chips { get; set; }
		public override Hand hand { get; set; }
		public override string name { get; set; }

		public CPUPlayer (string n)
		{
			name = n;
		}

		public override Tuple<Decision, int> GetDecision()
		{
			return Tuple.Create (Decision.CHECK, 0);
		}
	}
}

