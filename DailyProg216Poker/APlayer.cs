using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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

			// Best Hand Init
			int bestHandScore = 0;
			string bestHandDesc = "";
			Hand bestHandHand = new Hand (null);
			List<List<Card>> handCombinations = new List<List<Card>> ();
			foreach (IEnumerable<Card> c in tableCards.Combinations (5))
			{
				handCombinations.Add (c.ToList ());
			}
			HandEvaluator hEval = new HandEvaluator ();
			int hsScore;
			string hsDesc;
			Hand currentHand;

			foreach (List<Card> h in handCombinations)
			{
				currentHand = new Hand (h);
				hEval.GetHandScore (currentHand, out hsScore, out hsDesc);
				if (hsScore > bestHandScore)
				{
					bestHandScore = hsScore;
					bestHandDesc = hsDesc;
					bestHandHand = currentHand;
				}
			}
			return Tuple.Create (bestHandScore, bestHandDesc, bestHandHand);
		}
	}
}

