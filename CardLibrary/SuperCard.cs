using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary {

    public abstract class SuperCard : IEquatable<SuperCard> {

		public Rank cardsRank { get; set; }
		public abstract Suit cardSuit { get; }
		public bool inPlay { get; set; }

		public abstract void Display();

		public bool Equals(SuperCard otherCard) {
			return (this.cardSuit == otherCard.cardSuit) ? true : false;
		}

    }

}
