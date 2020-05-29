
namespace TxtName.AspNetCore.UEditor.Core.Services.Uploads
{
    /// <summary>
    /// 上传文件完成后返回的数据结构
    /// </summary>
    public class UploadOutput:UEditorOutput
    {
        /// <summary>
        /// 上传成功后的图片相对路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string Original { get; set; }
    }
}
