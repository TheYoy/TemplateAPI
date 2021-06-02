using System.Collections.Generic;
using System.Linq;
using APITemplate.Model.ExternalResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APITemplate.Helper.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid)
            {
                List<string> messageList = new List<string>();
                foreach (var validate in context.ModelState.ToList())
                {
                    foreach (var error in validate.Value.Errors)
                    {
                        messageList.Add(error.ErrorMessage);
                    }
                }

                ErrorData errorData = new ErrorData();
                var err = new Error();
                err.code = "API400";
                err.message = "Invalid Request Model.";
                err.type = "API";
                err.parameter = Newtonsoft.Json.JsonConvert.SerializeObject(messageList);
                errorData.error = err;

                context.HttpContext.Response.Headers.Add("reponsemessage", "Invalid Request Model.");
                context.HttpContext.Response.Headers.Add("reponsecode", "Invalid Request Model.");
                context.HttpContext.Response.Headers.Add("responsedatasource", context.HttpContext.Request.Path.Value.Substring(1).Replace("/","_"));

                context.Result = new OkObjectResult(errorData);
            }
        }
    }
}