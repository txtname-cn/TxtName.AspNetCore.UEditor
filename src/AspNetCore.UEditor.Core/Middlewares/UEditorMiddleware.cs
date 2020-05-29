using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Threading.Tasks;
using TxtName.AspNetCore.UEditor.Core.Services;

namespace TxtName.AspNetCore.UEditor.Core.Middlewares
{
    /// <summary>
    /// 处理UEditor请求中间件
    /// </summary>
    public class UEditorMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public UEditorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="serviceCenter"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, UEditorServiceCenter serviceCenter)
        {
            try
            {
                await _next(httpContext);

                var data = await serviceCenter.DoActionAsync();
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.Headers.Add("content-type", "application/json");
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    }), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                await HandleError(httpContext,ex);
            }
        }

        private async Task HandleError(HttpContext httpContext,Exception ex)
        {
            UEditorOutput output = new UEditorOutput
            {
                State = ex.Message,
                Error = ex is UEditorServiceException ? ((UEditorServiceException)ex).ErrorDetail : ex.StackTrace
            };

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";

            //写入响应流
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(output, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            }));
        }
    }
}
