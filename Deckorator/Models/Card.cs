using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckorator.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public string Name { get; set; }

        public Card(string name)
        {
            this.Name = name;
        }
    }
}
