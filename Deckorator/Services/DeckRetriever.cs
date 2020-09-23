using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Deckorator.Services
{
    public class DeckRetriever
    {
        private readonly HttpClient httpClient;

        public DeckRetriever(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetRandomDeckUrl()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.scryfall.com/cards/random?q=is%3Acommander");
            string json = await response.Content.ReadAsStringAsync();
            JObject cardDict = JsonConvert.DeserializeObject<JObject>(json);

            return cardDict.GetValue("name").ToString();
        }
    }
}
