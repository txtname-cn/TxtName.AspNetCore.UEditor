
namespace TxtName.AspNetCore.UEditor.Core.Services
{
    /// <summary>
    /// 返回给UEditor编辑器的基本数据结构
    /// </summary>
    public class UEditorOutput
    {
        /// <summary>
        /// 返回的状态或错误码
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 调试用错误信息
        /// </summary>
        public string Error { get; set; }
    }
}
