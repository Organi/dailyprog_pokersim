using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DailyProg216Poker
{
	public class HandEvaluator
	{
		int[] s;
		int[] v;

		public HandEvaluator ()
		{
		}

		public Tuple<int, string> GetHandScore(Hand h)
		{
			// Reset Suits and Values
			this.s = new int[Enum.GetNames(typeof(Suit)).Length+1];
			this.v = new int[Enum.GetNames (typeof(Value)).Length+1];
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

			int StraightFlushScore = this.CheckStraightFlush ();
			if (StraightFlushScore > 0)
			{
				return Tuple.Create (StraightFlushScore, "Straight Flush");
			}
			int FourOfAKindScore = this.CheckFourOfAKind ();
			if (FourOfAKindScore > 0)
			{
				return Tuple.Create (FourOfAKindScore, "4 of a Kind");
			}
			int FullHouseScore = this.CheckFullHouse ();
			if (FullHouseScore > 0)
			{
				return Tuple.Create (FullHouseScore, "Full House");
			}
			int FlushScore = this.CheckFlush ();
			if (FlushScore > 0)
			{
				return Tuple.Create (FlushScore, "Flush");
			}
			int StraightScore = this.CheckStraight ();
			if (StraightScore > 0)
			{
				return Tuple.Create (StraightScore, "Straight");
			}
			int ThreeOfAKindScore = this.CheckThreeOfAKind ();
			if (ThreeOfAKindScore > 0)
			{
				return Tuple.Create (ThreeOfAKindScore, "3 of a Kind");
			}
			int TwoPairScore = this.CheckTwoPair ();
			if (TwoPairScore > 0)
			{
				return Tuple.Create (TwoPairScore, "Two Pair");
			}
			int PairScore = this.CheckPair ();
			if (PairScore > 0)
			{
				return Tuple.Create (PairScore, "Pair");
			}
			int HighCardScore = this.CheckHighCard ();

			/*Console.WriteLine ();
			Console.WriteLine (h.GetString (true));
			Console.WriteLine ();
			Console.WriteLine ("Straight Flush: " + StraightFlushScore);
			Console.WriteLine ("4 of a Kind: " + FourOfAKindScore);
			Console.WriteLine ("Full House: " + FullHouseScore);
			Console.WriteLine ("Flush: " + FlushScore);
			Console.WriteLine ("Straight: " + StraightScore);
			Console.WriteLine ("3 of a Kind: " + ThreeOfAKindScore);
			Console.WriteLine ("Two Pair: " + TwoPairScore);
			Console.WriteLine ("Pair: " + PairScore);
			Console.WriteLine ("High Card: " + HighCardScore);*/

			return Tuple.Create (HighCardScore, "High Card");
		}

		private int CheckStraightFlush()
		{
			if (this.CheckFlush () == 0)
			{
				return 0;
			}
			int score = 900000;

			score += this.CheckStraight ();

			if (score > 900000)
			{
				return score;
			}
			else
			{
				return 0;
			}
		}

		private int CheckFourOfAKind()
		{
			if (v.Max () == 4)
			{
				int score = 800000;
				// Add the value of the card that we have 4 of
				score += v.ToList().IndexOf(4);
				return score;
			}
			else {
				return 0;
			}
		}

		private int CheckFullHouse()
		{
			if (v.Max () == 3 && v.Contains (2))
			{
				int score = 700000;
				score += v.ToList ().IndexOf (3) * 10;
				score += v.ToList ().IndexOf (2) * 10;
				return score;
			}
			else {
				return 0;
			}
		}

		private int CheckFlush()
		{
			if (s.Max () == 5)
			{
				return 600000;
			}
			else {
				return 0;
			}
		}

		private int CheckStraight()
		{
			int score = 500000;
			// A,2,3,4,5
			if (v[1] == 1 && v[2] == 1 && v[3] == 1 && v[4] == 1 && v[5] == 1)
			{
				score += 5;
			}
			// 2,3,4,5,6
			else if (v[2] == 1 && v[3] == 1 && v[4] == 1 && v[5] == 1 && v[6] == 1)
			{
				score += 6;
			}
			// 3,4,5,6,7
			else if (v[3] == 1 && v[4] == 1 && v[5] == 1 && v[6] == 1 && v[7] == 1)
			{
				score += 7;
			}
			// 4,5,6,7,8
			else if (v[4] == 1 && v[5] == 1 && v[6] == 1 && v[7] == 1 && v[8] == 1)
			{
				score += 8;
			}
			// 5,6,7,8,9
			else if (v[5] == 1 && v[6] == 1 && v[7] == 1 && v[8] == 1 && v[9] == 1)
			{
				score += 9;
			}
			// 6,7,8,9,10
			else if (v[6] == 1 && v[7] == 1 && v[8] == 1 && v[9] == 1 && v[10] == 1)
			{
				score += 10;
			}
			// 7,8,9,10,J
			else if (v[7] == 1 && v[8] == 1 && v[9] == 1 && v[10] == 1 && v[11] == 1)
			{
				score += 11;
			}
			// 8,9,10,J,Q
			else if (v[8] == 1 && v[9] == 1 && v[10] == 1 && v[11] == 1 && v[12] == 1)
			{
				score += 12;
			}
			// 9,10,J,Q,K
			else if (v[9] == 1 && v[10] == 1 && v[11] == 1 && v[12] == 1 && v[13] == 1)
			{
				score += 13;
			}
			// 10,J,Q,K,A
			else if (v[10] == 1 && v[11] == 1 && v[12] == 1 && v[13] == 1 && v[1] == 1)
			{
				score += 14;
			}
			else {
				return 0;
			}
			return score;
		}

		private int CheckThreeOfAKind()
		{
			if (v.Max () == 3)
			{
				int score = 400000;
				// Add the value of the card that we have 4 of
				score += v.ToList().IndexOf(3);
				return score;
			}
			else {
				return 0;
			}
		}

		private int CheckTwoPair()
		{
			if (v.Max () == 2)
			{
				int score = 0;
				int NoOfPairs = 0;
				int[] PairValues = new int[2];
				for (int i = 0; i < v.Length; i++)
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
					score += PairValues.Max () * 10;
					score += PairValues.Min ();
					return score;
				}
				else {
					// We don't have two pairs
					return 0;
				}
			}
			else
			{
				return 0;
			}
		}

		private int CheckPair()
		{
			if (v.Max () == 2)
			{
				int score = 200000;
				int idx = v.ToList ().IndexOf (v.Max ());
				score += idx;
				return score;
			}
			else
			{
				return 0;
			}
		}

		private int CheckHighCard()
		{
			if (v.Max() == 1)
			{
				int score = 100000;
				score += v.ToList().IndexOf(v.Max());
				return score;
			}
			else {
				return 0;
			}
		}
	}
}

