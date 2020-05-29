using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TxtName.AspNetCore.UEditor.Core.Services.Configs
{
    /// <summary>
    /// UEditor编辑器配置服务，用户编辑器初始化时获取配置
    /// <para>此为默认实现，如有需要可重写</para>
    /// </summary>
    public class UEditorConfigService : UEditorService, IUEditorConfigService
    {
        /// <summary>
        /// 返回UEditor编辑器配置
        /// </summary>
        /// <returns></returns>
        public virtual async Task<object> GetConfigAsync()
        {
            return await Task.FromResult(UEditorConfig);
        }

        /// <summary>
        /// 重新加载配置
        /// </summary>
        /// <returns></returns>
        public virtual async Task ReloadConfigAsync()
        {
            await Task.Run(()=> {
                JsonConvert.PopulateObject(File.ReadAllText(ServiceConfig.ConfigFileName, Encoding.UTF8),UEditorConfig);
            });
        }
    }
}
