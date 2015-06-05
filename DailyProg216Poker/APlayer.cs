using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyProg216Poker
{
	public abstract class APlayer : IPlayer
	{
		public abstract int chips { get; set; }
		public abstract Hand hand { get; set; }
		public abstract string name { get; set; }

		public abstract Tuple<Decision, int> GetDecision ();

		public Tuple<int, string, Hand> GetBestHand(List<Card> tableCards)
		{
			// Add the players hand
			tableCards.AddRange (hand.getHand ());
			Tuple<int, string, Hand> bestHand = new Tuple<int, string, Hand>(0, "", new Hand(null));
			List<List<Card>> handCombinations = new List<List<Card>> ();
			foreach (IEnumerable<Card> c in tableCards.Combinations (5))
			{
				handCombinations.Add (c.ToList ());
			}
			HandEvaluator hEval = new HandEvaluator ();
			foreach (List<Card> h in handCombinations)
			{
				Tuple<int, string> hs = hEval.GetHandScore (new Hand (h));
				if (hs.Item1 > bestHand.Item1)
				{
					bestHand = Tuple.Create (hs.Item1, hs.Item2, new Hand (h));
				}
			}
			return bestHand;
		}
	}
}

