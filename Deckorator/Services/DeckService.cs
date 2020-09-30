using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using Deckorator.Models;
using System.Linq;

namespace Deckorator.Services
{
    public class DeckService
    {
        private readonly HttpClient httpClient;
        private readonly AppDbContext appDb;
        private readonly string source = "https://tappedout.net{0}";
        private readonly string deckListQuery = "/mtg-decks/search/?general={0}&page={1}";
        private readonly int startingPage = 1;

        public DeckService(HttpClient httpClient, AppDbContext appDb)
        {
            this.httpClient = httpClient;
            this.appDb = appDb;
        }

        public async Task<KeyValuePair<string, string>> GetRandomDeckUrl()
        {
            HttpResponseMessage commanderResponse = await httpClient.GetAsync("https://api.scryfall.com/cards/random?q=is%3Acommander");
            string json = await commanderResponse.Content.ReadAsStringAsync();
            string commander = JsonConvert.DeserializeObject<JObject>(json).GetValue("name").ToString().ToLower().Replace("\"", String.Empty).Replace(",", String.Empty).Replace("\'", String.Empty).Replace(" ", "-");
            Random random = new Random();
            
            HttpResponseMessage decksResponse;
            int currMaxPage = startingPage;
            do
            {
                string decksQuery = string.Format(deckListQuery, new string[] { commander, (random.Next(currMaxPage) + 1).ToString() });
                currMaxPage /= 2;
                Console.WriteLine("TappedOut query: " + string.Format(source, decksQuery));
                decksResponse = await httpClient.GetAsync(string.Format(source, decksQuery));
            } while (!decksResponse.IsSuccessStatusCode && currMaxPage > 0);

            string htmlString = await decksResponse.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlString);

            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//h3[@class='name deck-wide-header']/a");
            if (nodes == null) return KeyValuePair.Create<string, string>(commander, "error");
            return KeyValuePair.Create<string, string>(commander, string.Format(source, nodes[random.Next(nodes.Count)].GetAttributeValue("href", "Error")));
        }

        public List<Deck> getTrainingDecks()
        {
            return appDb.Decks.ToList();
        }

        public async Task<bool> SaveDeck(string deckUrl, double rating)
        {
            HttpResponseMessage deckResponse = await httpClient.GetAsync(deckUrl + "?fmt=txt");
            Deck deck = new Deck(deckResponse.Content.Headers.ContentDisposition.FileName, deckUrl, rating);

            string deckString = await deckResponse.Content.ReadAsStringAsync();
            using (StringReader reader = new StringReader(deckString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string trimmed = line.Trim();
                    if (trimmed.Length == 0) continue;
                    deck.Cards.Add(new Card(trimmed));
                }
            }

            appDb.Add(deck);
            appDb.SaveChanges();

            Console.WriteLine(deck.ToString());
            return true;
        }
    }
}