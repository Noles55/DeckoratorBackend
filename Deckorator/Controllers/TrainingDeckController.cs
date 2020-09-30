using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deckorator.Models;
using Deckorator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deckorator.Controllers
{
    public class TrainingDecksController : Controller
    {
        private readonly DeckService deckService;

        public TrainingDecksController(DeckService deckRetriever)
        {
            this.deckService = deckRetriever;
        }

        public async Task<IActionResult> RandomDeck()
        {
            KeyValuePair<string, string> deckInfo = await deckService.GetRandomDeckUrl();

            if (deckInfo.Value.Equals("error")) return RedirectToAction("RandomDeck");
            ViewData["Commander"] = deckInfo.Key;
            ViewData["DeckLink"] = deckInfo.Value;
          
            return View();
        }

        public IActionResult All()
        {
            return View(new TrainingDeckList(deckService.getTrainingDecks()));
        }

        [HttpPost]
        public async Task<IActionResult> RateDeck(string deckUrl, double rating)
        {
            Console.WriteLine("Rating deck: url = " + deckUrl + ", rating = " + rating);
            await deckService.SaveDeck(deckUrl, rating);
            return RedirectToAction("RandomDeck");
        }
    }
}