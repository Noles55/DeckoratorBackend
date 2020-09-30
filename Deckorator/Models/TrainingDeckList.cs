using System.Collections.Generic;

namespace Deckorator.Models
{
    public class TrainingDeckList
    {
        public List<Deck> Decks { get; }

        public TrainingDeckList(List<Deck> decks)
        {
            this.Decks = decks;
        }
    }
}
