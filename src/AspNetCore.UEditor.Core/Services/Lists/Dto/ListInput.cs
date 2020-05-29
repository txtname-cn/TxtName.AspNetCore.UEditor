using System.Collections.Generic;

namespace TxtName.AspNetCore.UEditor.Core.Services.Lists
{
    /// <summary>
    /// 列出文件的公共参数
    /// </summary>
    public class ListInput
    {
        /// <summary>
        /// 开始索引
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 请求数量
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 需要列出文件的目录
        /// </summary>
        public string ListPath { get; set; }
        /// <summary>
        /// 允许列出的文件类型
        /// </summary>
        public List<string> AllowFileExtensions { get; set; }
    }
}
