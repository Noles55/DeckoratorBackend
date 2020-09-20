using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

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

        public string GetRandomDeckUrl()
        {
            Exceptional<List<Card>> result = mtgApi.Where(x => x.GameFormat, "Commander")
                             .Where(x => x.SuperTypes, new string[] { "Legendary", "Creature" })
                             .All();
            
            if (result.IsSuccess)
            {
                return JsonConvert.SerializeObject(result.Value);
            }
            else throw new Exception();
        }
    }
}
