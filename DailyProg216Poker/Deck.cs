using System;
using System.Collections.Generic;

namespace DailyProg216Poker
{
	public class Deck
	{
		private List<Card> deck = new List<Card>();

		public Deck ()
		{
			foreach (Suit s in Enum.GetValues (typeof(Suit)))
			{
				foreach (Value v in Enum.GetValues (typeof(Value)))
				{
					this.deck.Add (new Card ((int)v, (int)s));
				}
			}
		}

		public int Count()
		{
			return this.deck.Count;
		}

		public void ShowInConsole()
		{
			foreach (Card c in this.deck)
			{
				Console.WriteLine (c.getName ());
			}
		}

		public void ShowShortInConsole()
		{
			Console.WriteLine ();
			foreach (Card c in this.deck)
			{
				Console.Write (c.getShortName() + " ");
			}
			Console.WriteLine ();
		}

		public List<Card> DrawNo(int num)
		{
			List<Card> cards = new List<Card> ();
			for (int i = 0; i < num; i++)
			{
				cards.Add (Draw ());
			}
			return cards;
		}

		public Card Draw()
		{
			try
			{
				Card c = this.deck [this.deck.Count - 1];
				this.deck.RemoveAt (this.deck.Count - 1);
				return c;
			}
			catch(Exception e)
			{
				if (this.deck.Count.Equals (0))
				{
					Console.WriteLine ("Unable to draw card, deck is empty");
					Console.WriteLine (e.Message);
					return new Card (1, 1);
				}
				else {
					Console.WriteLine ("Unable to draw card");
					Console.WriteLine (e.Message);
					return new Card (1, 1);
				}
			}
		}

		public void Shuffle()
		{
			List<Card> shuffledDeck = new List<Card> ();
			Random r = new Random ();
			int deckSize = this.deck.Count;
			for (int i=0; i<deckSize; i++)
			{
				int pos = r.Next (this.deck.Count);
				shuffledDeck.Add (this.deck [pos]);
				this.deck.RemoveAt (pos);
			}
			this.deck = shuffledDeck;
		}
	}
}

