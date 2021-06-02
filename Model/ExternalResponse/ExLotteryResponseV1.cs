using System.Collections.Generic;
using APITemplate.DependencyInjecyions.Microservices.Lottery;
using APITemplate.Model.InternalResponse;

namespace APITemplate.Model.ExternalResponse
{
    public class ExLotteryResponseV1 : BaseResponse
    {
        public LotteryResponseV1 data { get; set; }
    }

}