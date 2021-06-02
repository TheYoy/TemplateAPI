using System.Collections.Generic;
using APITemplate.DependencyInjecyions.Microservices.Tlog.Response;
using APITemplate.Model.InternalResponse;

namespace APITemplate.Model.ExternalResponse
{
    public class ExTlogPlaceResponseV1 : BaseResponse
    {
        public List<TlogPlaceResponseV1> data { get; set; }
    }

}