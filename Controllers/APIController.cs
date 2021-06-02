using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using APITemplate.Infrastructure.ActionResult;
using APITemplate.Model.ExternalResponse;
using APITemplate.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APITemplate.Controllers
{
    public class APIController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly apiProcess _apiprocess;
        public APIController(IHttpContextAccessor httpContextAccessor,apiProcess apiprocess)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiprocess = apiprocess;
        }

        [HttpGet]
        [Description("Thongchut.T | Check alive")]
        [Route("alive")]
        public ActionResult Alive()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            Alive _result = new Alive();
            _result.alive = true;
            _result.version = version;
            return Ok(_result);
        }

        [HttpGet]
        [Description("Thongchut.T | GetTlogPlace")]
        [Route("/v1/getTlogPlace")]
        public async Task<ActionResult> getTlogPlaceAsync()
        {
           try
           {
                var _result = await _apiprocess.taskGetTlogPlaceV1.ApplyAsync();
                   if(!_result.Result || _result.ResponseCode != ((int)StatusCodes.Status200OK).ToString())
                   {
                       Response.Headers.Add("resoonsecode",_result.ResponseCode);
                       Response.Headers.Add("responsedatasource",_result.ResponseDataSource);
                       Response.Headers.Add("resoonsemessage",_result.RespnseMessage.Replace(System.Environment.NewLine,string.Empty));
                       return Ok(_result.Error);
                   }
                   else
                   {
                       Response.Headers.Add("resoonsecode",_result.ResponseCode);
                       Response.Headers.Add("responsedatasource",_result.ResponseDataSource);
                       Response.Headers.Add("resoonsemessage",_result.RespnseMessage.Replace(System.Environment.NewLine,string.Empty));
                       return Ok(_result.data);
                   }
           }
           catch (Exception ex)
           {
               return new InternalServerErrorObjectResult(ex);
           }
        }

        [HttpGet]
        [Description("Thongchut.T | GetLottery")]
        [Route("/v1/getLottery/{date}")]
        public async Task<ActionResult> getLotteryV1Async(string date)
        {
           try
           {
                var _result = await _apiprocess.taskGetLotteryV1.ApplyAsync(date);
                   if(!_result.Result || _result.ResponseCode != ((int)StatusCodes.Status200OK).ToString())
                   {
                       Response.Headers.Add("resoonsecode",_result.ResponseCode);
                       Response.Headers.Add("responsedatasource",_result.ResponseDataSource);
                       Response.Headers.Add("resoonsemessage",_result.RespnseMessage.Replace(System.Environment.NewLine,string.Empty));
                       return Ok(_result.Error);
                   }
                   else
                   {
                       Response.Headers.Add("resoonsecode",_result.ResponseCode);
                       Response.Headers.Add("responsedatasource",_result.ResponseDataSource);
                       Response.Headers.Add("resoonsemessage",_result.RespnseMessage.Replace(System.Environment.NewLine,string.Empty));
                       return Ok(_result.data);
                   }
              
           }
           catch (Exception ex)
           {
               return new InternalServerErrorObjectResult(ex);
           }
        }
    }
}
