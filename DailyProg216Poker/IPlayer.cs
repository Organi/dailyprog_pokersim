using System;
using System.Collections.Generic;

namespace DailyProg216Poker
{
	public interface IPlayer
	{
		int chips { get; set; }
		Hand hand { get; set; }
		string name { get; set; }
		Tuple<Decision, int> GetDecision();
		Tuple<int, string> GetBestHand(List<Card> tableCards);
	}
}

