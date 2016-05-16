using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// custom
using CardLibrary;

namespace ChrisO_Poker {

	class Program {

		static void Main(string[] args) {
			int howManyCards = 3;
			int balance = 10;

			CardSet myDeck = new CardSet();
			
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.BackgroundColor = ConsoleColor.DarkBlue;

			Console.WriteLine("Welcome to the poker game!");
			Console.WriteLine("You have ${0} and each bet will be $1.", balance);
			Console.WriteLine("Press any key when you are ready to start.");
			Console.ReadKey();

			while (balance > 0) {
				// clear the cards in use for new hand
				myDeck.ResetUsage();

				// clear the console
				Console.Clear();

				// deal the hands
				SuperCard[] computerHand = myDeck.GetCards(howManyCards); 
				SuperCard[] playersHand = myDeck.GetCards(howManyCards);

				// display the hands
				DisplayHands(computerHand, playersHand);

				// draw single card for the player and the computer
				PlayerDrawsOne(playersHand, myDeck);
				ComputerDrawsOne(computerHand, myDeck);

				// display the updated hands
				DisplayHands(computerHand, playersHand);

				// see if the player won
				bool won = CompareHands(computerHand, playersHand);

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.BackgroundColor = ConsoleColor.DarkBlue;

				if (won) {
					balance += 1;
					Console.WriteLine("Congratulations, you won!.");
				} else {
					balance -= 1;
					Console.WriteLine("You lost.");
				}

				if (balance == 0) break; 

				Console.WriteLine("You have ${0} in your balance. Type Enter for another hand.", balance);
				Console.ReadLine();
			}

			// clear the console
			Console.Clear();
			Console.WriteLine("You don't have any money left to bet. Press Enter to exit.");

			// stop program
			Console.ReadLine();
		}

		// display the hands for the user
		public static void DisplayHands(SuperCard[] compHand, SuperCard[] playerHand) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.BackgroundColor = ConsoleColor.DarkBlue;

			Console.WriteLine("DEALER HAND");

			foreach (SuperCard card in compHand) {
				card.Display();
			}

			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("YOUR HAND");

			foreach (SuperCard card in playerHand) {
				card.Display();
			}
		}

		// compare the hands to see who won
		public static bool CompareHands(SuperCard[] compHand, SuperCard[] playerHand) {
			if (Flush(compHand)) { // computer flush trumps all
				// for debugging
				Console.WriteLine("It's a flush! - Computer");

				return false;
			} else if (Flush(playerHand)) { // player flush trumps all EXCEPT a computer flush
				// for debugging
				Console.WriteLine("It's a flush! - Player");

				return true;
			} else { // if neither get a flush, go by hand rank value
				int compTotal = 0;
				int playerTotal = 0;

				foreach (SuperCard card in compHand) {
					int val = (int)card.cardsRank;
					compTotal += val;
				}

				foreach (SuperCard card in playerHand) {
					int val = (int)card.cardsRank;
					playerTotal += val;
				}

				return (playerTotal > compTotal) ? true : false;
			}

		}

		// draw a new card for the player
		public static void PlayerDrawsOne(SuperCard[] playerHand, CardSet deck) {
			int cardVal;
			bool isValid = false;
			bool replace = false;
			bool result;
			int handLength = playerHand.Length;

			Console.WriteLine();

			Console.Write("Please enter which card to replace (1-{0} OR 0 for none): ", handLength);
			result = Int32.TryParse(Console.ReadLine(), out cardVal);

			while (!isValid) {
				// check if the value entered was a number
				if (!result) {
					Console.Write("\nPlease enter a valid card number (1-{0} OR 0 for none): ", handLength);
					result = Int32.TryParse(Console.ReadLine(), out cardVal);
				} else {
					if (cardVal >= 1 && cardVal <= handLength) {
						isValid = true;
						replace = true;
					} else if (cardVal == 0) {
						isValid = true;
					} else {
						Console.Write("\nPlease enter a valid card number (1-{0} OR 0 for none): ", handLength);
						result = Int32.TryParse(Console.ReadLine(), out cardVal);
					}
				}
			}

			if (replace) {
				// subract one for correct array assignment
				cardVal -= 1;
				playerHand[cardVal] = deck.GetOneCard();
			}

			Console.WriteLine();
		}

		// draw a new card for the computer
		public static void ComputerDrawsOne(SuperCard[] computerHand, CardSet deck) {
			// draw a new card if the computer's hand is not a flush
			if (!Flush(computerHand)) {
				int lowestCardRank = 15;
				int lowestCardIndex = 0;
				int curCardRank;

				for (int i = 0; i < computerHand.Length - 1; i++) {
					curCardRank = (int)computerHand[i].cardsRank;

					// if the current card rank is lower than the lowest rank, replace with the current rank
					if (curCardRank < lowestCardRank) {
						lowestCardRank = curCardRank;
						lowestCardIndex = i;
					}
				}

				// if the lowest card rank is lower than 7, replace that card with a random new card
				if (lowestCardRank < 7) {
					computerHand[lowestCardIndex] = deck.GetOneCard();
				}
			}
		}

		// check if the hand is a flush
		private static bool Flush(SuperCard[] hand) {
			bool isFlush = true;

			// the default card will always the 0 index so start the counter at 1
			for (int i = 1; i < hand.Length; i++) {
				if (!hand[0].Equals(hand[i])) {
					isFlush = false;
					break;
				}
			}

			return isFlush;
		}

	}

}
