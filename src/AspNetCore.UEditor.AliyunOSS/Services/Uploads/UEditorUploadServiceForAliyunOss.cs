using Aliyun.OSS;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TxtName.AspNetCore.UEditor.Core;
using TxtName.AspNetCore.UEditor.Core.Services.Uploads;

namespace TxtName.AspNetCore.UEditor.AliyunOSS.Services.Uploads
{
    /// <summary>
    /// UEditor上传至阿里云OSS服务
    /// </summary>
    public class UEditorUploadServiceForAliyunOss:UEditorUploadService
    {
        private readonly AliyunOssServiceConfig _ossConfig;
        private readonly OssClient _ossClient;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ossConfig"></param>
        /// <param name="ossClient"></param>
        public UEditorUploadServiceForAliyunOss(AliyunOssServiceConfig ossConfig,OssClient ossClient)
        {
            _ossConfig = ossConfig;
            _ossClient = ossClient;
        }

        /// <summary>
        /// 上传文件的具体实现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UploadOutput> UploadCoreAsync(UploadCoreInput input)
        {
            var output = new UploadOutput();
            //存入OSS的对象名称
            var objectName = $"{_ossConfig.ObjectNamePrefix}/{input.FileName}";
            try
            {
                using (MemoryStream ms = new MemoryStream(input.FileBytes))
                {
                    await Task.Run(() => {
                        var putResult = _ossClient.PutObject(_ossConfig.BucketName, objectName, ms);
                        if (putResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            output.State = "SUCCESS";
                            output.Url = $"{(string.IsNullOrWhiteSpace(_ossConfig.CustomerDomain) ? $"{_ossConfig.BucketName}.{_ossConfig.EndPoint}/{objectName}" : $"{_ossConfig.CustomerDomain}/{objectName}")}";
                            output.Original = input.OriginalFileName;
                        }
                        else
                        {
                            throw new UEditorServiceException("上传至OSS失败", new StreamReader(putResult.ResponseStream, Encoding.UTF8).ReadToEnd());
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new UEditorServiceException("上传至OSS操作失败", ex.Message);
            }

            return await Task.FromResult(output);
        }
    }
}
