using System.Threading.Tasks;

namespace TxtName.AspNetCore.UEditor.Core.Services.Uploads
{
    /// <summary>
    /// UEditor上传文件服务
    /// </summary>
    public interface IUEditorUploadService:IUEditorService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        Task<UploadOutput> UploadAsync();
        /// <summary>
        /// 封装上传文件需要的参数
        /// </summary>
        /// <returns></returns>
        Task<UploadCoreInput> GetUploadParamAsync();
        /// <summary>
        /// 上传文件的具体实现
        /// </summary>
        /// <returns></returns>
        Task<UploadOutput> UploadCoreAsync(UploadCoreInput input);
    }
}
