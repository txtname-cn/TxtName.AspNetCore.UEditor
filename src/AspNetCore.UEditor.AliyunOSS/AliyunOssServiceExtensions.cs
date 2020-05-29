using Aliyun.OSS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TxtName.AspNetCore.UEditor.AliyunOSS.Services.Lists;
using TxtName.AspNetCore.UEditor.AliyunOSS.Services.Uploads;
using TxtName.AspNetCore.UEditor.Core;
using TxtName.AspNetCore.UEditor.Core.Services.Lists;
using TxtName.AspNetCore.UEditor.Core.Services.Uploads;

namespace TxtName.AspNetCore.UEditor.AliyunOSS
{
    /// <summary>
    /// 使用阿里云OSS服务
    /// </summary>
    public static class AliyunOssServiceExtensions
    {
        /// <summary>
        /// 使用阿里云OSS服务
        /// </summary>
        /// <param name="serviceConfig"></param>
        public static void UseAliyunOss(this UEditorServiceConfig serviceConfig)
        {
            serviceConfig.UseAliyunOss(config=> { });
        }

        /// <summary>
        /// 使用阿里云OSS服务
        /// </summary>
        /// <param name="serviceConfig"></param>
        /// <param name="config">阿里云OSS配置</param>
        public static void UseAliyunOss(this UEditorServiceConfig serviceConfig, Action<AliyunOssServiceConfig> config)
        {
            //替换默认的上传和列出文件服务
            serviceConfig.RegisterService<IUEditorUploadService,UEditorUploadServiceForAliyunOss>();
            serviceConfig.RegisterService<IUEditorListService, UEditorListServiceForAliyunOss>();

            //读取阿里云OSS配置
            ServiceLocator.ServiceCollection.AddSingleton((serviceProvider)=> {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var ossConfig = new AliyunOssServiceConfig() { 
                    AccessKey = configuration["UEditorAspNetCore:Service:AliyunOss:AccessKey"],
                    AccessKeyId = configuration["UEditorAspNetCore:Service:AliyunOss:AccessKeyId"],
                    EndPoint = configuration["UEditorAspNetCore:Service:AliyunOss:EndPoint"],
                    BucketName = configuration["UEditorAspNetCore:Service:AliyunOss:BucketName"],
                    CustomerDomain = configuration["UEditorAspNetCore:Service:AliyunOss:CustomerDomain"],
                    ObjectNamePrefix = configuration["UEditorAspNetCore:Service:AliyunOss:ObjectNamePrefix"],
                };
                config.Invoke(ossConfig);

                return ossConfig;
            });

            //注册阿里云OSS SDK
            ServiceLocator.ServiceCollection.AddSingleton((serviceProvider) => {
                var ossConfig = serviceProvider.GetService<AliyunOssServiceConfig>();
                return new OssClient(ossConfig.EndPoint, ossConfig.AccessKeyId, ossConfig.AccessKey);
            });
        }
    }
}
