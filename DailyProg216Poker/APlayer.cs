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

		public Tuple<int, string> GetBestHand(List<Card> tableCards)
		{
			// Add the players hand
			tableCards.AddRange (this.hand.getHand ());

			List<int> handScores = new List<int> ();
			List<string> handStrings = new List<string> ();
			List<List<Card>> handCombinations = new List<List<Card>> ();
			foreach (IEnumerable<Card> c in tableCards.Combinations (5))
			{
				handCombinations.Add (c.ToList ());
			}
			HandEvaluator hEval = new HandEvaluator ();
			foreach (List<Card> h in handCombinations)
			{
				Tuple<int, string> hs = hEval.GetHandScore (new Hand (h));
				handScores.Add (hs.Item1);
				handStrings.Add (hs.Item2);
			}
			return Tuple.Create (handScores.Max (), handStrings[handScores.IndexOf(handScores.Max())]);
		}
	}
}

