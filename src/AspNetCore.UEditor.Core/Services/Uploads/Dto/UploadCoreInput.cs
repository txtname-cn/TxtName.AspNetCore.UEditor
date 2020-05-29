
namespace TxtName.AspNetCore.UEditor.Core.Services.Uploads
{
    /// <summary>
    /// 上传文件需要的参数
    /// </summary>
    public class UploadCoreInput
    {
        /// <summary>
        /// URL前缀
        /// </summary>
        public string UrlPrefix { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginalFileName { get; set; }
        /// <summary>
        /// 格式化后的文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] FileBytes { get; set; }
    }
}
