using System;
using System.Collections.Generic;

namespace DailyProg216Poker
{
	public class Hand
	{
		private List<Card> hand = new List<Card>();

		public Hand (List<Card> h)
		{
			this.hand = h;
		}

		public List<Card> getHand()
		{
			return this.hand;
		}

		public int Size()
		{
			return this.hand.Count;
		}

		public void Add(Card c)
		{
			this.hand.Add (c);
		}

		public void Remove(Card c)
		{
			this.hand.Remove (c);
		}
	}
}

