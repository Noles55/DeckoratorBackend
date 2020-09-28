using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deckorator.Models
{
    public class Deck
    {
        public string Name { get; set; }
        public string Url { get; }
        public double Rating { get; set; }
        public List<string> Cards { get; set; }

        public Deck(string name, string url, double rating = -1)
        {
            this.Name = name;
            this.Url = url;
            this.Rating = rating;
            this.Cards = new List<string>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Name: {0}\nURL: {1}\nRating: {2}\n", new string[] { Name, Url, Rating.ToString() });
            foreach (string card in Cards)
            {
                builder.AppendFormat("\t{0}\n", card);
            }

            return builder.ToString();
        }
    }
}
