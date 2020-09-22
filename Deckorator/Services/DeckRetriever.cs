using Newtonsoft.Json;
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
            var response = await httpClient.GetAsync("https://api.scryfall.com/cards/random?q=is%3Acommander");
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
