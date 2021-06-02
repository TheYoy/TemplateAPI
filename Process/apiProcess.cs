using APITemplate.DependencyInjecyions.Microservices;
using APITemplate.Process.TaskGetLottery.v1_0;
using Microsoft.AspNetCore.Http;

namespace APITemplate.Process
{
    public class apiProcess
    {
        public readonly TaskGetLotteryV1 taskGetLotteryV1;
        public readonly TaskGetTlogPlaceV1 taskGetTlogPlaceV1;

        public apiProcess(IHttpContextAccessor httpContextAccessor,Microservices microservices)
        {
            taskGetLotteryV1 = new TaskGetLotteryV1(httpContextAccessor, microservices);
            taskGetTlogPlaceV1 = new TaskGetTlogPlaceV1(httpContextAccessor, microservices);
        }
    }
}