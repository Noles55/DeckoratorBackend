using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Deckorator.Services
{
    public class DeckRetriever
    {
        private readonly HttpClient httpClient;
        private readonly string deckSourceQuery = "https://tappedout.net/mtg-decks/search/?general={0}&page={1}";
        private readonly int startingPage = 20;

        public DeckRetriever(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetRandomDeckUrl()
        {
            HttpResponseMessage commanderResponse = await httpClient.GetAsync("https://api.scryfall.com/cards/random?q=is%3Acommander");
            string json = await commanderResponse.Content.ReadAsStringAsync();
            string commander = JsonConvert.DeserializeObject<JObject>(json).GetValue("name").ToString().ToLower().Replace("\"", String.Empty).Replace(",", String.Empty).Replace(" ", "-");
            Random random = new Random();

            HttpResponseMessage decksResponse;
            int currMaxPage = startingPage;
            do
            {
                string decksQuery = string.Format(deckSourceQuery, new string[] { commander, (random.Next(currMaxPage) + 1).ToString() });
                currMaxPage /= 2;
                Console.WriteLine("TappedOut query: " + decksQuery);
                decksResponse = await httpClient.GetAsync(decksQuery);
            } while (!decksResponse.IsSuccessStatusCode && currMaxPage > 0);
           
            string htmlString = await decksResponse.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlString);

            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//h3[@class='name deck-wide-header']/a");

            return "";
        }
    }
}
//*[@id="body"]/div[7]/div/div[1]/div[9]/div/div[2]/h3/a