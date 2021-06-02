using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace APITemplate.DependencyInjecyions.Microservices.Lottery
{
    public class lotteryAPI
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;


        public lotteryAPI(IHttpContextAccessor httpContextAccessor, IHttpClientFactory ClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientFactory = ClientFactory;
        }

        public async Task<LotteryResponseV1> getLottery (string date)
        {
            
            var contentype = new StringContent(
                "",
                Encoding.UTF8,
                "application/json"
                );
                
                Console.WriteLine(date);
            var client = _clientFactory.CreateClient("serviceclient");
            client.DefaultRequestHeaders.Add("X-API-KEY", "544c14182078b72e4cab902a6348d689");
            var response = await client.PostAsync("https://api.krupreecha.com/" + date,contentype);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"reponse form [api] Lottery not return 200 in statuscode (return : {response.StatusCode.ToString()})") { Source = "getLottery"};
            }
            if(response.Headers == null)
            {
                throw new Exception($"reponse form [api] Lottery not return headers") { Source = "getLottery"};
            }

            var content = new LotteryResponseV1();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);  

                var res = await response.Content.ReadAsStringAsync();
               
                content = JsonConvert.DeserializeObject<LotteryResponseV1>(res);
            }

            return content;
         }

    }
}