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
using APITemplate.DependencyInjecyions.Microservices.Tlog.Response;

namespace APITemplate.DependencyInjecyions.Microservices.Tlog
{
    public class TlogAPI
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public TlogAPI(IHttpContextAccessor httpContextAccessor, IHttpClientFactory ClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientFactory = ClientFactory;
        }

        public async Task<List<TlogPlaceResponseV1>> getTlogPlace ()
        {
            var contenttype = new StringContent(
                "",
                Encoding.UTF8,
                "application/json");
            var client = _clientFactory.CreateClient("serviceclient");
            var response = await client.PostAsync("http://www.tlog.uspaces.in.th/API-Tlog/place/list/",contenttype);


            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"reponse form [api] Lottery not return 200 in statuscode (return : {response.StatusCode.ToString()})") { Source = "getTlogPlace"};
            }
            if(response.Headers == null)
            {
                throw new Exception($"reponse form [api] Lottery not return headers") { Source = "getTlogPlace"};
            }

             var content = new List<TlogPlaceResponseV1>();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);  

                var res = await response.Content.ReadAsStringAsync();

                content = JsonConvert.DeserializeObject<List<TlogPlaceResponseV1>>(res) as List<TlogPlaceResponseV1>;
            }

            return content;
         }

    }
}