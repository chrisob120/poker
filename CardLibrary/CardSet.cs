using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary {

	public class CardSet {

		public SuperCard[] cardArray;
		public Random random = new Random();

		public CardSet() {
			cardArray = new SuperCard[52];

			int cnt = 0;

			// Spades
			for (int i = 2; i <= 14; i++) {
				cardArray[cnt] = new CardSpade((Rank)i);

				cnt++;
			}

			// Hearts
			for (int i = 2; i <= 14; i++) {
				cardArray[cnt] = new CardHeart((Rank)i);

				cnt++;
			}

		    // Diamonds
			for (int i = 2; i <= 14; i++) {
				cardArray[cnt] = new CardDiamond((Rank)i);

				cnt++;
			}

			// Clubs
			for (int i = 2; i <= 14; i++) {
				cardArray[cnt] = new CardClub((Rank)i);

				cnt++;
			}
		}

		// gets the number of cards set by the user for the hand
		public SuperCard[] GetCards(int numCards) {
			SuperCard[] returnArr = new SuperCard[numCards];
			int index = 0;
			bool isTaken;

			for(int i = 0; i <= numCards - 1; i++) {
				isTaken = true;

				while (isTaken) {
					index = random.Next(0, cardArray.Length - 1);

					// if card isn't inPlay, put it in play
					if (cardArray[index].inPlay == false) {
						isTaken = false;
						cardArray[index].inPlay = true;

						returnArr[i] = cardArray[index];
					}
				}
			}

			return returnArr;
		}

		// gets one card from the desk of 52
		public SuperCard GetOneCard() {
			SuperCard returnCard;
			int index = 0;
			bool isTaken = true;

			do {
				index = random.Next(0, cardArray.Length - 1);
				returnCard = cardArray[index];

				// if card isn't inPlay, put it in play
				if (cardArray[index].inPlay == false) {
					isTaken = false;
					cardArray[index].inPlay = true;
				}
			} while(isTaken);

			return returnCard;
		}

		// reset all cards to unused when a new hand is dealt
		public void ResetUsage() {
			foreach (SuperCard card in cardArray) {
				card.inPlay = false;
			}
		}
		
	}

}
