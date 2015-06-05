using System;

namespace DailyProg216Poker
{
	public class PlayerFactory
	{
		public IPlayer Create(char Type, string name)
		{
			if (Type == 'H')
			{
				return new HPlayer (name);
			}
			else if (Type == 'C')
			{
				return new CPUPlayer (name);
			}
			else {
				throw new Exception ("Unknown Player Type");
			}
		}
	}
}

