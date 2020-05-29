using System.Collections.Generic;

namespace TxtName.AspNetCore.UEditor.Core
{
    /// <summary>
    /// 服务配置
    /// </summary>
    public class UEditorServiceConfig
    {
        /// <summary>
        /// UEditor服务请求路径，默认为 /api/ueditor
        /// </summary>
        public string EntryPath { get; set; } = "/api/ueditor";
        /// <summary>
        /// UEditor配置文件路径，默认为 wwwroot/lib/ueditor/config.json
        /// </summary>
        public string ConfigFileName { get; set; } = $"wwwroot/lib/ueditor/config.json";
        /// <summary>
        /// WebRootPath
        /// </summary>
        public string WebRootPath { get; internal set; }
        /// <summary>
        /// 服务列表
        /// </summary>
        internal List<UEditorRegisterService> RegisteredServices { get; set; }
    }
}
