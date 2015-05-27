using System;

namespace DailyProg216Poker
{
	public interface IPlayer
	{
		int chips { get; set; }
		Hand hand { get; set; }
		string name { get; set; }
	}
}

