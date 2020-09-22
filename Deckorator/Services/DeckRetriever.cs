using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Deckorator.Services
{
    public class DeckRetriever
    {
        private readonly HttpClient httpClient;
        private readonly CardService mtgApi;

        public DeckRetriever(HttpClient httpClient, CardService mtgApi)
        {
            this.httpClient = httpClient;
            this.mtgApi = mtgApi;
        }

        //public string GetRandomDeckUrl()
        //{
        //    Exceptional<List<Card>> creaturesRequest = mtgApi.Where(x => x.GameFormat, "Commander")
        //                     .Where(x => x.SuperTypes, new string[] { "Legendary", "Creature" })
        //                     .All();

        //    Exceptional<List<Card>> planeswalkersRequest = mtgApi.Where(x => x.GameFormat, "Commander").Where(x => x.SuperTypes, new string[] { "Planeswalker" }).Where(x => x.Text, "can be your commander").All();
  

        //    if (creaturesRequest.IsSuccess && planeswalkersRequest.IsSuccess)
        //    {
        //        creaturesRequest.Value.AddRange(planeswalkersRequest.Value);
        //        List<Card> commanders = new List<Card>(creaturesRequest.Value);
        //        Random rng = new Random();

        //        return JsonConvert.SerializeObject(commanders[rng.Next(commanders.Count)]);
        //    }
        //    else throw new Exception();
        //}
        
        public async Task<string> GetRandomDeckUrl()
        {
            var response = await httpClient.GetAsync("https://api.scryfall.com/cards/random?q=is%3Acommander");
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
