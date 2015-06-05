using System;
using System.Linq;

namespace DailyProg216Poker
{
	public class HandEvaluator
	{
		int[] s;
		int[] v;

		public Tuple<int, string> GetHandScore(Hand h)
		{
			// Reset Suits and Values
			s = new int[Enum.GetNames(typeof(Suit)).Length+1];
			v = new int[Enum.GetNames (typeof(Value)).Length+1];
			for (int i = 0; i < s.Length; i++)
			{
				s [i] = 0;
			}
			for (int i = 0; i < v.Length; i++)
			{
				v [i] = 0;
			}

			// Set suits and values for the hand
			foreach (Card c in h.getHand ())
			{
				s [(int)c.suit]++;
				v [(int)c.value]++;
			}

			Tuple<int, string> StraightFlushScore = CheckStraightFlush ();
			if (StraightFlushScore.Item1 > 0)
			{
				return StraightFlushScore;
			}
			Tuple<int, string> FourOfAKindScore = CheckFourOfAKind ();
			if (FourOfAKindScore.Item1 > 0)
			{
				return FourOfAKindScore;
			}
			Tuple<int, string> FullHouseScore = CheckFullHouse ();
			if (FullHouseScore.Item1 > 0)
			{
				return FullHouseScore;
			}
			Tuple<int, string> FlushScore = CheckFlush ();
			if (FlushScore.Item1 > 0)
			{
				return FlushScore;
			}
			Tuple<int, string> StraightScore = CheckStraight ();
			if (StraightScore.Item1 > 0)
			{
				return StraightScore;
			}
			Tuple<int, string> ThreeOfAKindScore = CheckThreeOfAKind ();
			if (ThreeOfAKindScore.Item1 > 0)
			{
				return ThreeOfAKindScore;
			}
			Tuple<int, string> TwoPairScore = CheckTwoPair ();
			if (TwoPairScore.Item1 > 0)
			{
				return TwoPairScore;
			}
			Tuple<int, string> PairScore = CheckPair ();
			if (PairScore.Item1 > 0)
			{
				return PairScore;
			}
			return CheckHighCard ();
		}

		private Tuple<int, string> CheckStraightFlush()
		{
			if (CheckFlush ().Item1 == 0)
			{
				return Tuple.Create(0, "");
			}
			int score = 900000;

			Tuple<int, string> straight = CheckStraight ();
			score += straight.Item1;

			if (score > 900000)
			{
				string description;
				if (straight.Item2.StartsWith ("Ace"))
				{
					description = "Royal Flush";
				}
				else
				{
					description = straight.Item2 + " Flush";
				}
				return Tuple.Create (score, description);
			}
			else
			{
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckFourOfAKind()
		{
			if (v.Max () == 4)
			{
				int score = 800000;
				// Add the value of the card that we have 4 of
				int idx = v.ToList().IndexOf(4);
				// Check for Ace
				if (idx == 1)
				{
					score += idx + v.Length;
				}
				else {
					score += v.ToList().IndexOf(4);
				}
				return Tuple.Create (score, "4 " + (Plurals) idx);
			}
			else {
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckFullHouse()
		{
			if (v.Max () == 3 && v.Contains (2))
			{
				int score = 700000;
				score += v.ToList ().IndexOf (3) * 10;
				score += v.ToList ().IndexOf (2) * 10;
				return Tuple.Create (score, "Full House: " + (Plurals)v.ToList ().IndexOf (3) + " over " + (Plurals)v.ToList ().IndexOf (2));
			}
			else {
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckFlush()
		{
			if (s.Max () == 5)
			{
				string idx = (Suit) s.ToList ().IndexOf (5) + "";
				idx = idx.ToLower ();
				idx = char.ToUpper (idx [0]) + idx.Substring (1);
				return Tuple.Create (600000, idx + " Flush");
			}
			else {
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckStraight()
		{
			int score = 500000;
			int card;
			// A,2,3,4,5
			if (v[1] == 1 && v[2] == 1 && v[3] == 1 && v[4] == 1 && v[5] == 1)
			{
				score += 5;
				card = 5;
			}
			// 2,3,4,5,6
			else if (v[2] == 1 && v[3] == 1 && v[4] == 1 && v[5] == 1 && v[6] == 1)
			{
				score += 6;
				card = 6;
			}
			// 3,4,5,6,7
			else if (v[3] == 1 && v[4] == 1 && v[5] == 1 && v[6] == 1 && v[7] == 1)
			{
				score += 7;
				card = 7;
			}
			// 4,5,6,7,8
			else if (v[4] == 1 && v[5] == 1 && v[6] == 1 && v[7] == 1 && v[8] == 1)
			{
				score += 8;
				card = 8;
			}
			// 5,6,7,8,9
			else if (v[5] == 1 && v[6] == 1 && v[7] == 1 && v[8] == 1 && v[9] == 1)
			{
				score += 9;
				card = 9;
			}
			// 6,7,8,9,10
			else if (v[6] == 1 && v[7] == 1 && v[8] == 1 && v[9] == 1 && v[10] == 1)
			{
				score += 10;
				card = 10;
			}
			// 7,8,9,10,J
			else if (v[7] == 1 && v[8] == 1 && v[9] == 1 && v[10] == 1 && v[11] == 1)
			{
				score += 11;
				card = 11;
			}
			// 8,9,10,J,Q
			else if (v[8] == 1 && v[9] == 1 && v[10] == 1 && v[11] == 1 && v[12] == 1)
			{
				score += 12;
				card = 12;
			}
			// 9,10,J,Q,K
			else if (v[9] == 1 && v[10] == 1 && v[11] == 1 && v[12] == 1 && v[13] == 1)
			{
				score += 13;
				card = 13;
			}
			// 10,J,Q,K,A
			else if (v[10] == 1 && v[11] == 1 && v[12] == 1 && v[13] == 1 && v[1] == 1)
			{
				score += 14;
				card = 1;
			}
			else {
				return Tuple.Create(0, "");
			}
			return Tuple.Create (score, (Value) card + " High Straight");
		}

		private Tuple<int, string> CheckThreeOfAKind()
		{
			if (v.Max () == 3)
			{
				int score = 400000;
				/*
				 * Add the value of the card that we have 3 of
				 * Note that unlike Pair and TwoPair we do not need
				 * to check for collisions because we only accept 5
				 * card hands. Therefore you can only have one max of 3.
				 */
				int idx = v.ToList().IndexOf(3);
				score += idx;
				return Tuple.Create (score, "3 " + (Plurals) idx);
			}
			else {
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckTwoPair()
		{
			if (v.Max () == 2)
			{
				int score = 0;
				int NoOfPairs = 0;
				int[] PairValues = new int[2];
				for (int i = 1; i < v.Length; i++)
				{
					if (v[i] == 2)
					{
						PairValues[NoOfPairs] = i;
						NoOfPairs++;
					}
				}
				if (NoOfPairs == 2)
				{
					score = 300000;
					// Check for Ace
					if (PairValues.Min() == 1)
					{
						score += PairValues.Max () * 10;
						score += PairValues.Min () * 10;
						return Tuple.Create (score, "Pair of "+(Plurals)PairValues.Min () + " over " + (Plurals)PairValues.Max ());
					}
					else {
						score += PairValues.Max () * 10;
						score += PairValues.Min ();
						return Tuple.Create (score, "Pair of "+(Plurals)PairValues.Max () + " over " + (Plurals)PairValues.Min ());						
					}
				}
				else {
					// We don't have two pairs
					return Tuple.Create(0, "");
				}
			}
			else
			{
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckPair()
		{
			if (v.Max () == 2)
			{
				int score = 200000;

				int addValue = 0;
				string card = "";
				for (int i = 1; i < v.Length; i++)
				{
					if (i == 1 && v[i] == v.Max ())
					{
						score += v.Length + 1; // Ace
						card = "Pair of " + (Plurals) i;
						break;
					}
					else if (v[i] == v.Max())
					{
						addValue = i;
					}
				}
				if (addValue > 0)
				{
					score += addValue;
					card = "Pair of " + (Plurals) addValue;
				}
				return Tuple.Create (score, card);
			}
			else
			{
				return Tuple.Create(0, "");
			}
		}

		private Tuple<int, string> CheckHighCard()
		{
			if (v.Max() == 1)
			{
				int score = 100000;
				int addValue = 0;
				string card = "";
				for (int i = 1; i < v.Length; i++)
				{
					if (i == 1 && v[i] == v.Max ())
					{
						score += v.Length + 1; // Ace
						card = (Value) i + " High";
						break;
					}
					else if (v[i] == v.Max())
					{
						addValue = i;
					}
				}
				if (addValue > 0)
				{
					score += addValue;
					card = (Value) addValue + " High";
				}
				return Tuple.Create (score, card);
			}
			else {
				return Tuple.Create(0, "");
			}
		}
	}
}

