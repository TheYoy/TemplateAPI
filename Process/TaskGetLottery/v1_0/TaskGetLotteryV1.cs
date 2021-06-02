using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APITemplate.DependencyInjecyions.Microservices;
using APITemplate.DependencyInjecyions.Microservices.Lottery;
using APITemplate.Model.ExternalResponse;
using Microsoft.AspNetCore.Http;

namespace APITemplate.Process.TaskGetLottery.v1_0
{
    public class TaskGetLotteryV1
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly lotteryAPI _lotteryAPI;
        public TaskGetLotteryV1(IHttpContextAccessor httpContextAccessor, Microservices microservices)
        {
            _httpContextAccessor = httpContextAccessor;
            _lotteryAPI = microservices.lotteryAPI;
        }

        public async Task<ExLotteryResponseV1> ApplyAsync(string date)
        {
            var tcs = new TaskCompletionSource<ExLotteryResponseV1>();
            try
            {
                ExLotteryResponseV1 lottery = new ExLotteryResponseV1();
                LotteryResponseV1 data = new LotteryResponseV1();
                data = await _lotteryAPI.getLottery(date);

                lottery.data = data;
                lottery.Result = true;
                lottery.ResponseCode = "200";
                lottery.RespnseMessage = "success";
                lottery.ResponseDataSource = "n/a";

                tcs.SetResult(lottery);
            }
            catch (Exception ex)
            {
                var response = new ExLotteryResponseV1();
                Error err = new Error();
                err.code = ((int)StatusCodes.Status500InternalServerError).ToString();
                err.type = "microservice";
                err.message = ex.Message;

                ErrorData error = new ErrorData();
                error.error = err;
                response.Error = error;
                response.ResponseDataSource = "";
                response.RespnseMessage = "lottery API V1 Error";
                response.ResponseCode = ((int)StatusCodes.Status500InternalServerError).ToString();
                response.Result = false;
                
                tcs.SetResult(response);
            }
           return await tcs.Task;
        }

    }
}