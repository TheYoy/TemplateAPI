using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APITemplate.DependencyInjecyions.Microservices;
using APITemplate.DependencyInjecyions.Microservices.Lottery;
using APITemplate.DependencyInjecyions.Microservices.Tlog;
using APITemplate.DependencyInjecyions.Microservices.Tlog.Response;
using APITemplate.Model.ExternalResponse;
using Microsoft.AspNetCore.Http;

namespace APITemplate.Process.TaskGetLottery.v1_0
{
    public class TaskGetTlogPlaceV1
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TlogAPI _tlogAPI;
        public TaskGetTlogPlaceV1(IHttpContextAccessor httpContextAccessor, Microservices microservices)
        {
            _httpContextAccessor = httpContextAccessor;
            _tlogAPI = microservices.tlogAPI;
        }

        public async Task<ExTlogPlaceResponseV1> ApplyAsync()
        {
            var tcs = new TaskCompletionSource<ExTlogPlaceResponseV1>();
            try
            {
                ExTlogPlaceResponseV1 place = new ExTlogPlaceResponseV1();
                List<TlogPlaceResponseV1> data = new List<TlogPlaceResponseV1>();
                data = await _tlogAPI.getTlogPlace();
                place.data = data;
                place.Result = true;
                place.ResponseCode = "200";
                place.RespnseMessage = "success";
                place.ResponseDataSource = "n/a";

                tcs.SetResult(place);
            }
            catch (Exception ex)
            {
                var response = new ExTlogPlaceResponseV1();
                Error err = new Error();
                err.code = ((int)StatusCodes.Status500InternalServerError).ToString();
                err.type = "microservice";
                err.message = ex.Message;

                ErrorData error = new ErrorData();
                error.error = err;
                response.Error = error;
                response.ResponseDataSource = "";
                response.RespnseMessage = "tlog API V1 Error";
                response.ResponseCode = ((int)StatusCodes.Status500InternalServerError).ToString();
                response.Result = false;

                tcs.SetResult(response);
            }
           return await tcs.Task;
        }

    }
}