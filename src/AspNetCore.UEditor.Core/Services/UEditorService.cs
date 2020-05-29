using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TxtName.AspNetCore.UEditor.Core.Services
{
    /// <summary>
    /// UEditor服务基类，提供了一些常用的属性
    /// </summary>
    public abstract class UEditorService : IUEditorService
    {
        /// <summary>
        /// UEditor服务配置
        /// </summary>
        protected UEditorServiceConfig ServiceConfig { get; }
        /// <summary>
        /// UEditor编辑器配置
        /// </summary>
        protected UEditorConfig UEditorConfig { get; }
        /// <summary>
        /// HttpContext
        /// </summary>
        protected HttpContext Context { get; }
        /// <summary>
        /// UEditor编辑器请求的服务
        /// </summary>
        protected string Action { get; }

        /// <summary>
        /// 
        /// </summary>
        protected UEditorService()
        {
            ServiceConfig = ServiceLocator.ServiceProvider.GetService<UEditorServiceConfig>();
            UEditorConfig = ServiceLocator.ServiceProvider.GetService<UEditorConfig>();
            Context = ServiceLocator.ServiceProvider.GetService<IHttpContextAccessor>().HttpContext;
            Action = Context.Request.Query["action"];
        }
    }
}
