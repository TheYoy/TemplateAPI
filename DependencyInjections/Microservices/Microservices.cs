using System.Net.Http;
using APITemplate.DependencyInjecyions.Microservices.Lottery;
using APITemplate.DependencyInjecyions.Microservices.Tlog;
using Microsoft.AspNetCore.Http;

namespace APITemplate.DependencyInjecyions.Microservices
{
    public class Microservices
    {
        public readonly lotteryAPI lotteryAPI;
        public readonly TlogAPI tlogAPI;

        public Microservices(IHttpContextAccessor httpContextAccessor , IHttpClientFactory ClientFactory)
        {
            lotteryAPI = new lotteryAPI(httpContextAccessor,ClientFactory);
            tlogAPI = new TlogAPI(httpContextAccessor,ClientFactory);
        }
    }
}