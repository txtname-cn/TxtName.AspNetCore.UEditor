using System.Threading.Tasks;

namespace TxtName.AspNetCore.UEditor.Core.Services.Configs
{
    /// <summary>
    /// UEditor编辑器配置服务，用户编辑器初始化时获取配置
    /// </summary>
    public interface IUEditorConfigService:IUEditorService
    {
        /// <summary>
        /// 返回UEditor编辑器配置
        /// </summary>
        /// <returns></returns>
        Task<object> GetConfigAsync();
        /// <summary>
        /// 重新加载配置
        /// </summary>
        /// <returns></returns>
        Task ReloadConfigAsync();
    }
}
