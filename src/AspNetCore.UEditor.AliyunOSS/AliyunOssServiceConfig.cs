
namespace TxtName.AspNetCore.UEditor.AliyunOSS
{
    /// <summary>
    /// 阿里云OSS配置
    /// </summary>
    public class AliyunOssServiceConfig
    {
        /// <summary>
        /// RAM AccessKeyId
        /// </summary>
        public string AccessKeyId { get; set; } = "";
        /// <summary>
        /// RAM AccessKey
        /// </summary>
        public string AccessKey { get; set; } = "";
        /// <summary>
        /// 存储空间名称
        /// </summary>
        public string BucketName { get; set; } = "";
        /// <summary>
        /// 域名映射
        /// </summary>
        public string CustomerDomain { get; set; }
        /// <summary>
        /// 接入点
        /// </summary>
        public string EndPoint { get; set; } = "";
        /// <summary>
        /// 路径前缀
        /// </summary>
        public string ObjectNamePrefix { get; set; } = "";
    }
}
