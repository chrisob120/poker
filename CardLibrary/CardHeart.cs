using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary {

	public class CardHeart : SuperCard {

		private Suit _cardSuit = Suit.Heart;

		// constructor
		public CardHeart(Rank cardRank) {
			cardsRank = cardRank;
		}

		public override Suit cardSuit {
			get { return _cardSuit; }
		}

		public override void Display() {
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(cardsRank + " of " + _cardSuit + "s ♥");
			Console.ResetColor();
		}

	}

}
