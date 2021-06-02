using APITemplate.Model.ExternalResponse;

namespace APITemplate.Model.InternalResponse
{
    public class BaseResponse
    {
        public bool Result { get; set; }
        public string ResponseCode { get; set; }
        public string RespnseMessage { get; set; }
        public string ResponseDataSource { get; set; }

        public ErrorData Error { get; set; }
    }
}