using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyProg216Poker
{
	public class Hand
	{
		private readonly List<Card> hand = new List<Card>();

		public Hand (List<Card> h)
		{
			hand = h;
		}

		public List<Card> getHand()
		{
			return hand;
		}

		public int Size()
		{
			return hand.Count;
		}

		public void Add(Card c)
		{
			hand.Add (c);
		}

		public void Remove(Card c)
		{
			hand.Remove (c);
		}

		public string GetString(bool useShortName = false)
		{
			string s = "";
			Card last = hand.Last ();
			foreach (Card c in hand)
			{
				if (useShortName)
				{
					s += c.getShortName ();
				}
				else {
					s += c.getName ();
				}
				if (c != last)
				{
					s += ", ";
				}
			}
			return s;
		}
	}
}

