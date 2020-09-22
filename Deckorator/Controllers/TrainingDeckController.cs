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
        private readonly DeckRetriever deckRetriever;

        public TrainingDeckController(DeckRetriever deckRetriever)
        {
            this.deckRetriever = deckRetriever;
        }

        public async Task<IActionResult> RandomDeck()
        {
            ViewData["Message"] = await deckRetriever.GetRandomDeckUrl();
            return View();
        }
    }
}