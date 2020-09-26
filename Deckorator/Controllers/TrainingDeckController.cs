using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deckorator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deckorator.Controllers
{
    public class TrainingDeckController : Controller
    {
        private readonly DeckService deckService;

        public TrainingDeckController(DeckService deckRetriever)
        {
            this.deckService = deckRetriever;
        }

        public async Task<IActionResult> RandomDeck()
        {
            KeyValuePair<string, string> deckInfo = await deckService.GetRandomDeckUrl();

            ViewData["Commander"] = deckInfo.Key;
            ViewData["DeckLink"] = deckInfo.Value;
          
            return View();
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