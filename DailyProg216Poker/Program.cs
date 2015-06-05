using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyProg216Poker
{
	public class Poker
	{
		Deck deck;
		List<IPlayer> players = new List<IPlayer> ();
		const bool includeHumanPlayer = true;
		const int noOfRounds = 200;
		const bool printRoundOutput = false;
		const bool storeRounds = false;
		const bool showStats = true;
		public List<Round> rounds = new List<Round> ();

		// Stats
		public double[] roundTimes = new double[noOfRounds];
		public TimeSpan programExecutionTime = new TimeSpan();

		int NoOfPlayers;
		string[] playerNames = {"Tom","Bob","Amy","Cat","Jake","Fred","Sara","Beth"};

		public static void Main (string[] args)
		{
			// Program Start
			DateTime programStart = DateTime.UtcNow;

			Poker p = new Poker ();

			p.NoOfPlayers = p.getNoOfPlayers ();

			// Game Set Up
			p.setUp (p.playerNames);

			// Run X Rounds
			for (int i = 0; i < Poker.noOfRounds; i++)
			{
				// Record Time
				DateTime begin = DateTime.UtcNow;

				// Start New Round
				Round r = new Round(p);

				// Process Round
				r.ProcessRound();

				if (Poker.printRoundOutput)
				{
					Console.WriteLine ();
					Console.WriteLine ();	
				}

				// Record Time
				DateTime end = DateTime.UtcNow;
				r.runTime = (end - begin).TotalMilliseconds;

				// Record Stats
				p.roundTimes[i] = r.runTime;

				if (Poker.storeRounds)
				{
					// Either Store the whole round
					p.rounds.Add (r);
				}
				else {
					// Or just store statistics
				}
			}

			// Program End
			DateTime programEnd = DateTime.UtcNow;
			p.programExecutionTime = programEnd - programStart;

			if (Poker.showStats)
			{
				p.calcStats ();
			}
		}

		public void setUp(string[] names)
		{
			try
			{
				// Add Players
				PlayerFactory pf = new PlayerFactory();

				// Check for human player
				if (Poker.includeHumanPlayer) {
					players.Add (pf.Create ('H', "John"));
				}
				// Add all other players
				for (int i = 0; i < NoOfPlayers - 1; i++)
				{
					players.Add (pf.Create ('C', names[i]));
				}
			}
			catch(Exception e)
			{
				Console.WriteLine (e.Message);
			}
		}

		public int getNoOfPlayers()
		{
			Console.Write ("Please enter the number of players [2-8]: ");
			try
			{
				int NumPlayers = int.Parse (Console.ReadLine ());
				if (NumPlayers >= 2 && NumPlayers <=8)
				{
					Console.WriteLine ();
					return NumPlayers;
				}
				else {
					throw new Exception ("Number of players must be between 2 and 8");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				return 0;
			}
		}

		public List<IPlayer> getPlayers()
		{
			return players;
		}

		public void setUpDeck()
		{
			// Make the deck
			deck = new Deck ();
			deck.Shuffle ();
		}

		public Deck getDeck()
		{
			return deck;
		}

		public bool getPrintRoundOutput()
		{
			return printRoundOutput;
		}

		public void calcStats()
		{
			double progExecutionTime = programExecutionTime.TotalMilliseconds;
			double progExecutionTimeSeconds = progExecutionTime / 1000;
			double roundExecutionTime = roundTimes.Sum ();
			double roundExecutionTimeSeconds = roundExecutionTime / 1000;
			double averageRoundTime = roundTimes.Average ();
			Console.WriteLine ("Total Program Execution Time: " + progExecutionTime + " ms.");
			Console.WriteLine ("Total Program Execution Time: " + progExecutionTimeSeconds + " seconds.");
			Console.WriteLine ("Total Round Execution Time: " + roundExecutionTime + " ms.");
			Console.WriteLine ("Total Round Execution Time: " + roundExecutionTimeSeconds + " seconds.");
			Console.WriteLine ("Average Round Execution Time: " + averageRoundTime + " ms.");
		}
	}

	/*
	 * Shamelessly stolen from:
	 * http://stackoverflow.com/questions/127704/algorithm-to-return-all-combinations-of-k-elements-from-n
	 * to calculate, in this case, 5 card hand combinations from 6 or 7 card lists
	 */
	public static class HandCombinations
	{
		public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
		{
			return k == 0 ? new[] { new T[0] } :
			elements.SelectMany((e, i) =>
				elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] {e}).Concat(c)));
		}
	}
}
