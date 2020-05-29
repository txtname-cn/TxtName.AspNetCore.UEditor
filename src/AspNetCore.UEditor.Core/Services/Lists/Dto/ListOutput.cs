using System.Collections.Generic;

namespace TxtName.AspNetCore.UEditor.Core.Services.Lists
{
    /// <summary>
    /// 列出文件后返回的数据结构
    /// </summary>
    public class ListOutput:UEditorOutput
    {
        /// <summary>
        /// 请求的数据大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 开始索引
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 文件列表
        /// </summary>
        public List<ListItemOutput> List { get; set; }
    }

    /// <summary>
    /// 文件
    /// </summary>
    public class ListItemOutput
    {
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 文件的唯一标识
        /// </summary>
        public string Key { get; set; }
    }
}
