using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DailyProg216Poker
{
	public class Round
	{
		readonly List<IPlayer> players;
		List<Card> flop;
		Card turn;
		Card river;
		Poker GameState;

		public Round (Poker gs)
		{
			this.players = gs.getPlayers ();
			this.GameState = gs;
		}

		public void ProcessRound ()
		{
			// Blinds
			//

			// Deal Hands
			this.DealHands ();

			// Print Hands
			this.ShowHands();

			// Bet Round
			this.BetRound ();

			// Flop
			this.DealFlop ();

			// Bet Round
			this.BetRound ();

			// Turn
			this.DealTurn ();

			// Bet Round
			this.BetRound ();

			// River
			this.DealRiver ();

			// Bet Round
			this.BetRound ();

			// Determine Winner
			this.DetermineWinner ();
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
			foreach(IPlayer p in this.players)
			{
				p.hand = new Hand (this.GameState.getDeck ().DrawNo (2));
			}
		}

		public void ShowHands ()
		{
			foreach(IPlayer p in this.players)
			{
				Console.WriteLine (p.name + " hand: " + p.hand.GetString (true));
			}
		}

		public void DealFlop()
		{
			// Burn Card
			this.GameState.getDeck ().Draw ();
			// Draw Flop
			this.flop = this.GameState.getDeck ().DrawNo (3);
			// Print Flop
			Console.WriteLine();
			Card last = this.flop.Last ();
			Console.Write ("Flop: ");
			this.flop.ForEach (delegate(Card c) {
				Console.Write(c.getShortName ());
				if (!c.Equals (last)) {
					Console.Write(", ");
				}
			});
			Console.WriteLine();
		}

		public void DealTurn()
		{
			// Burn Card
			this.GameState.getDeck ().Draw ();
			// Draw Turn
			this.turn = this.GameState.getDeck ().Draw ();
			// Print Turn
			Console.WriteLine("Turn: " + this.turn.getShortName ());
		}

		public void DealRiver()
		{
			// Burn Card
			this.GameState.getDeck ().Draw ();
			// Draw River
			this.river = this.GameState.getDeck ().Draw ();
			// Print River
			Console.WriteLine("River: " + this.river.getShortName ());
		}

		public void DetermineWinner()
		{
			Tuple<int, string, Hand> winningHandScore = new Tuple<int, string, Hand>(0, "", new Hand(null));
			IPlayer winningPlayer = new HPlayer ("");
			foreach (IPlayer p in this.players)
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
			Console.WriteLine ();
			Console.WriteLine ("Winning Player: " + winningPlayer.name);
			Console.WriteLine ("Winning Hand: " + winningHandScore.Item3.GetString (true));
			Console.WriteLine (winningHandScore.Item2);
		}
	}
}

