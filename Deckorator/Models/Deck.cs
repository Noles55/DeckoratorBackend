using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deckorator.Models
{
    public class Deck
    {
        public int DeckId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Rating { get; set; }
        public List<Card> Cards { get; set; }

        public Deck(string name, string url, double rating = -1)
        {
            this.Name = name;
            this.Url = url;
            this.Rating = rating;
            this.Cards = new List<Card>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Name: {0}\nURL: {1}\nRating: {2}\n", new string[] { Name, Url, Rating.ToString() });
            foreach (Card card in Cards)
            {
                builder.AppendFormat("\t{0}\n", card.Name);
            }

            return builder.ToString();
        }
    }
}
