using System;
using System.Linq;
using System.Diagnostics;

namespace DailyProg216Poker
{
	public class HandEvaluator
	{
		int[] s;
		int[] v;
		// Hand Scores
		private int StraightFlushScore;
		private int FourOfAKindScore;
		private int FullHouseScore;
		private int FlushScore;
		private int StraightScore;
		private int ThreeOfAKindScore;
		private int TwoPairScore;
		private int PairScore;
		private int HighCardScore;
		// Counts
		private int MaxValueCount;
		private int MaxSuitCount;
		// Hand Strings
		private string StraightFlushDescription;
		private string FourOfAKindDescription;
		private string FullHouseDescription;
		private string FlushDescription;
		private string StraightDescription;
		private string ThreeOfAKindDescription;
		private string TwoPairDescription;
		private string PairDescription;
		private string HighCardDescription;

		public int GetHandScore(Hand h, out int handScore, out string handDesc)
		{
			// Reset Suits and Values
			// We assume for the sake of speed that there are 4 Suits and 13 Values
			// to avoid counting the Enums which is costly.
			// We +1 to these to avoid using the 0 index
			//s = new int[Enum.GetNames(typeof(Suit)).Length+1];
			//v = new int[Enum.GetNames (typeof(Value)).Length+1];
			s = new int[5];
			v = new int[14];

			for (int i = 0; i < s.Length; i++)
			{
				s [i] = 0;
			}
			for (int i = 0; i < v.Length; i++)
			{
				v [i] = 0;
			}

			// Set suits and values for the hand
			MaxSuitCount = 0;
			MaxValueCount = 0;
			foreach (Card c in h.getHand ())
			{
				s [(int)c.suit]++;
				v [(int)c.value]++;
				// Set Maximum Values
				if (s [(int)c.suit] > MaxSuitCount)
					MaxSuitCount = s [(int)c.suit];
				if (v [(int)c.value] > MaxValueCount)
					MaxValueCount = v [(int)c.value];
			}

			StraightFlushScore = CheckStraightFlush ();
			if (StraightFlushScore > 0)
			{
				handScore = StraightFlushScore;
				handDesc = StraightFlushDescription;
				return handScore;
			}

			FourOfAKindScore = CheckFourOfAKind ();
			if (FourOfAKindScore > 0)
			{
				handScore = FourOfAKindScore;
				handDesc = FourOfAKindDescription;
				return handScore;
			}

			FullHouseScore = CheckFullHouse ();
			if (FullHouseScore > 0)
			{
				handScore = FullHouseScore;
				handDesc = FullHouseDescription;
				return handScore;
			}

			// FlushScore will already be set from CheckStraightFlush
			if (FlushScore > 0)
			{
				handScore = FlushScore;
				handDesc = FlushDescription;
				return handScore;
			}

			StraightScore = CheckStraight ();
			if (StraightScore > 0)
			{
				handScore = StraightScore;
				handDesc = StraightDescription;
				return handScore;
			}

			ThreeOfAKindScore = CheckThreeOfAKind ();
			if (ThreeOfAKindScore > 0)
			{
				handScore = ThreeOfAKindScore;
				handDesc = ThreeOfAKindDescription;
				return handScore;
			}

			TwoPairScore = CheckTwoPair ();
			if (TwoPairScore > 0)
			{
				handScore = TwoPairScore;
				handDesc = TwoPairDescription;
				return handScore;
			}

			PairScore = CheckPair ();
			if (PairScore > 0)
			{
				handScore = PairScore;
				handDesc = PairDescription;
				return handScore;
			}

			HighCardScore = CheckHighCard ();
			handScore = HighCardScore;
			handDesc = HighCardDescription;
			return handScore;
		}

		public HandType GetHandType(int score)
		{
			if (score >= 900000)
				return HandType.STRAIGHT_FLUSH;
			else if (score >= 800000)
				return HandType.FOUR_OF_A_KIND;
			else if (score >= 700000)
				return HandType.FULL_HOUSE;
			else if (score >= 600000)
				return HandType.FLUSH;
			else if (score >= 500000)
				return HandType.STRAIGHT;
			else if (score >= 400000)
				return HandType.THREE_OF_A_KIND;
			else if (score >= 300000)
				return HandType.TWO_PAIR;
			else if (score >= 200000)
				return HandType.PAIR;
			else
				return HandType.HIGH_CARD;
		}

		private int CheckStraightFlush()
		{
			// Set FlushScore - don't need to recalculate this
			FlushScore = CheckFlush ();
			if (FlushScore == 0)
			{
				StraightFlushDescription = "";
				return 0;
			}
			int score = 900000;

			// Set StraightScore
			StraightScore = CheckStraight ();
			score += StraightScore;
			if (score > 900000)
			{
				if (StraightDescription.StartsWith ("Ace"))
				{
					StraightFlushDescription = "Royal Flush";
				}
				else
				{
					StraightFlushDescription = StraightDescription + " Flush";
				}
				return score;
			}
			else
			{
				StraightFlushDescription = "";
				return 0;
			}
		}

		private int CheckFourOfAKind()
		{
			if (MaxValueCount == 4)
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
					score += idx;
				}
				FourOfAKindDescription = "4 " + References.GetPluralString(idx);
				return score;
			}
			else {
				FourOfAKindDescription = "";
				return 0;
			}
		}

		private int CheckFullHouse()
		{
			if (MaxValueCount == 3 && v.Contains (2))
			{
				int score = 700000;
				int idx3 = v.ToList ().IndexOf (3);
				int idx2 = v.ToList ().IndexOf (2);
				score += idx3 * 10;
				score += idx2 * 10;
				FullHouseDescription = "Full House: " + References.GetPluralString(idx3) + " over " + References.GetPluralString(idx2);
				return score;
			}
			else {
				FullHouseDescription = "";
				return 0;
			}
		}

		private int CheckFlush()
		{
			if (MaxSuitCount == 5)
			{
				string idx = (Suit) s.ToList ().IndexOf (5) + "";
				idx = idx.ToLower ();
				idx = char.ToUpper (idx [0]) + idx.Substring (1);
				FlushDescription = idx + " Flush";
				return 600000;
			}
			else {
				FlushDescription = "";
				return 0;
			}
		}

		private int CheckStraight()
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
				StraightDescription = "";
				return 0;
			}
			StraightDescription = References.GetValueString(card) + " High Straight";
			return score;
		}

		private int CheckThreeOfAKind()
		{
			if (MaxValueCount == 3)
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
				ThreeOfAKindDescription = "3 " + References.GetValueString(idx);
				return score;
			}
			else {
				ThreeOfAKindDescription = "";
				return 0;
			}
		}

		private int CheckTwoPair()
		{
			int score = 0;
			if (MaxValueCount == 2)
			{
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
					int PairMax;
					int PairMin;
					// Faster than PairValues.Min and .Max
					if (PairValues[0] > PairValues[1])
					{
						PairMax = PairValues [0];
						PairMin = PairValues [1];
					}
					else
					{
						PairMax = PairValues [1];
						PairMin = PairValues [0];
					}
					// Check for Ace
					if (PairMin == 1)
					{
						score += PairMax * 10;
						score += PairMin * 10;
						TwoPairDescription = "Pair of " + References.GetPluralString(PairMin) + " over " + References.GetPluralString(PairMax);
					}
					else {
						score += PairMax * 10;
						score += PairMin;
						TwoPairDescription = "Pair of "+ References.GetPluralString(PairMax) + " over " + References.GetPluralString(PairMin);
					}
				}
				else {
					// We don't have two pairs
					TwoPairDescription = "";
				}
			}
			else
			{
				TwoPairDescription = "";
			}
			return score;
		}

		private int CheckPair()
		{
			int score = 0;
			if (MaxValueCount == 2)
			{
				score = 200000;

				int addValue = 0;
				// Check Ace First
				if (v[1] == MaxValueCount)
				{
					score += v.Length + 1; // Ace
					PairDescription = "Pair of Aces";
				}
				else
				{
					// Not an Ace pair so start loop on Pair of Twos
					for (int i = 2; i < v.Length; i++)
					{
						if (v[i] == MaxValueCount)
						{
							addValue = i;
						}
					}
					if (addValue > 0)
					{
						score += addValue;
						PairDescription = "Pair of " + References.GetPluralString(addValue);
					}
				}
			}
			return score;
		}

		private int CheckHighCard()
		{
			int score = 0;
			if (MaxValueCount == 1)
			{
				score = 100000;
				int addValue = 0;
				if (v[1] == MaxValueCount)
				{
					score += v.Length + 1; // Ace
					HighCardDescription = "Ace High";
				}
				else
				{
					for (int i = 2; i < v.Length; i++)
					{
						if (v[i] == MaxValueCount)
						{
							addValue = i;
						}
					}
					if (addValue > 0)
					{
						score += addValue;
						HighCardDescription = References.GetValueString (addValue) + " High";
					}
				}
			}
			return score;
		}
	}
}

