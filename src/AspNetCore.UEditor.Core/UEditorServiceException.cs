using System;

namespace TxtName.AspNetCore.UEditor.Core
{
    /// <summary>
    /// UEditor编辑器服务异常对象
    /// </summary>
    public class UEditorServiceException:Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">返回给UEditor的错误信息</param>
        /// <param name="errorDetail">用于调试的错误信息</param>
        public UEditorServiceException(string message, string errorDetail = "") : base(message)
        {
            ErrorDetail = errorDetail;
        }

        /// <summary>
        /// 用于调试的错误信息
        /// </summary>
        public string ErrorDetail { get; }
    }
}
