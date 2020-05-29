using System.Threading.Tasks;

namespace TxtName.AspNetCore.UEditor.Core.Services.Lists
{
    /// <summary>
    /// UEditor列出文件服务
    /// </summary>
    public interface IUEditorListService:IUEditorService
    {
        /// <summary>
        /// 列出文件的公共操作，参数封装
        /// </summary>
        /// <returns></returns>
        Task<ListOutput> ListAsync();
        /// <summary>
        /// 封装列出文件需要的参数
        /// </summary>
        /// <returns></returns>
        Task<ListInput> GetInputParamAsync();
        /// <summary>
        /// 列出文件的具体实现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListOutput> ListCoreAsync(ListInput input);
    }
}
