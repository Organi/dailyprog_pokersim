using System;

namespace DailyProg216Poker
{
	public class Player
	{
		public int chips { get; set; }
		public Hand hand { get; set; }
		public string name { get; set; }

		public Player (string n)
		{
			this.name = n;
		}
	}
}

