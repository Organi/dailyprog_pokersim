using System;
using System.Collections.Generic;
using System.Linq;

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
			// Deal Hands
			this.DealHands ();

			// Print Hands
			this.ShowHands();

			// Flop
			this.DealFlop ();

			// Turn
			this.DealTurn ();

			// River
			this.DealRiver ();
		
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
				Console.WriteLine (p.name + " hand: " + p.hand.GetString ());
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
				Console.Write(c.getName ());
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
			Console.WriteLine("Turn: " + this.turn.getName ());
		}

		public void DealRiver()
		{
			// Burn Card
			this.GameState.getDeck ().Draw ();
			// Draw River
			this.river = this.GameState.getDeck ().Draw ();
			// Print River
			Console.WriteLine("River: " + this.river.getName ());
		}
	}
}

