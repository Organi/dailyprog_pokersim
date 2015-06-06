using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace DailyProg216Poker
{
	public class Round
	{
		readonly List<IPlayer> players;
		List<Card> flop;
		Card turn;
		Card river;
		readonly Poker GameState;
		public long runTime { get; set; }
		public IPlayer winningPlayer;
		public HandType winningHandType;

		public Round (Poker gs)
		{
			players = gs.getPlayers ();
			GameState = gs;
			gs.setUpDeck ();
		}

		public void ProcessRound ()
		{
			// Blinds
			//

			// Deal Hands
			DealHands ();

			// Print Hands
			ShowHands ();

			// Bet Round
			BetRound ();

			// Flop
			DealFlop ();

			// Bet Round
			BetRound ();

			// Turn
			DealTurn ();

			// Bet Round
			BetRound ();

			// River
			DealRiver ();

			// Bet Round
			BetRound ();

			// Determine Winner
			DetermineWinner ();
		}

		public void BetRound()
		{
			// To-Do: Bet Logic
		}

		private bool CheckPlayers()
		{
			// To-Do: Check Players betting
			return true;
		}

		public void DealHands()
		{
			foreach(IPlayer p in players)
			{
				p.hand = new Hand (GameState.getDeck ().DrawNo (2));
			}
		}

		public void ShowHands ()
		{
			if (GameState.getPrintRoundOutput ())
			{
				foreach(IPlayer p in players)
				{
					Console.WriteLine (p.name + " hand: " + p.hand.GetString (true));
				}				
			}
		}

		public void DealFlop()
		{
			// Burn Card
			GameState.getDeck ().Draw ();
			// Draw Flop
			flop = GameState.getDeck ().DrawNo (3);
			// Print Flop
			if (GameState.getPrintRoundOutput ())
			{
				Console.WriteLine();
				Card last = flop.Last ();
				Console.Write ("Flop: ");
				flop.ForEach (delegate(Card c) {
					Console.Write(c.getShortName ());
					if (!c.Equals (last)) {
						Console.Write(", ");
					}
				});
				Console.WriteLine();				
			}
		}

		public void DealTurn()
		{
			// Burn Card
			GameState.getDeck ().Draw ();
			// Draw Turn
			turn = GameState.getDeck ().Draw ();
			// Print Turn
			if (GameState.getPrintRoundOutput ())
			{
				Console.WriteLine("Turn: " + turn.getShortName ());				
			}
		}

		public void DealRiver()
		{
			// Burn Card
			GameState.getDeck ().Draw ();
			// Draw River
			river = GameState.getDeck ().Draw ();
			// Print River
			if (GameState.getPrintRoundOutput ())
			{
				Console.WriteLine("River: " + river.getShortName ());				
			}
		}

		public void DetermineWinner()
		{
			Tuple<int, string, Hand> winningHandScore = new Tuple<int, string, Hand>(0, "", new Hand(null));
			foreach (IPlayer p in players)
			{
				// Create a list of the cards on the table
				List<Card> tableCards = new List<Card> ();
				tableCards.AddRange (flop);
				tableCards.Add (turn);
				tableCards.Add (river);

				// Get the players best hand
				Tuple<int, string, Hand> tmp = p.GetBestHand (tableCards);
				// Check if the current tmp hand is the best one so far
				if (tmp.Item1 > winningHandScore.Item1)
				{
					winningHandScore = tmp;
					winningPlayer = p;
				}
			}

			// Save Winner
			HandEvaluator hEval = new HandEvaluator ();
			winningHandType = hEval.GetHandType (winningHandScore.Item1);

			if (GameState.getPrintRoundOutput ())
			{
				Console.WriteLine ();
				Console.WriteLine ("Winning Player: " + winningPlayer.name);
				Console.WriteLine ("Winning Hand: " + winningHandScore.Item3.GetString (true));
				Console.WriteLine (winningHandScore.Item2);				
			}
		}
	}
}

