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
					deck.Add (new Card ((int)v, (int)s));
				}
			}
		}

		public int Count()
		{
			return deck.Count;
		}

		public void ShowInConsole()
		{
			foreach (Card c in deck)
			{
				Console.WriteLine (c.getName ());
			}
		}

		public void ShowShortInConsole()
		{
			Console.WriteLine ();
			foreach (Card c in deck)
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
				Card c = deck [deck.Count - 1];
				deck.RemoveAt (deck.Count - 1);
				return c;
			}
			catch(Exception e)
			{
				if (deck.Count.Equals (0))
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
			int deckSize = deck.Count;
			for (int i=0; i<deckSize; i++)
			{
				int pos = r.Next (deck.Count);
				shuffledDeck.Add (deck [pos]);
				deck.RemoveAt (pos);
			}
			deck = shuffledDeck;
		}
	}
}

