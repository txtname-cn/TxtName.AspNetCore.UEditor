using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TxtName.AspNetCore.UEditor.Core;
using TxtName.AspNetCore.UEditor.Core.Services.Lists;

namespace TxtName.AspNetCore.UEditor.AliyunOSS.Services.Lists
{
    /// <summary>
    /// UEditor列出OSS上文件的服务
    /// </summary>
    public class UEditorListServiceForAliyunOss:UEditorListService
    {
        private readonly AliyunOssServiceConfig _ossConfig;
        private readonly OssClient _ossClient;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ossConfig"></param>
        /// <param name="ossClient"></param>
        public UEditorListServiceForAliyunOss(AliyunOssServiceConfig ossConfig, OssClient ossClient)
        {
            _ossConfig = ossConfig;
            _ossClient = ossClient;
        }

        /// <summary>
        /// 列出文件的具体实现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ListOutput> ListCoreAsync(ListInput input)
        {
            var output = new ListOutput() { 
                Size = input.Size,
                Start = input.Start
            };

            try
            {
                //非首次请求时获取上次保存的分页标记
                var marker = "";
                if (input.Start > 0)
                {
                    marker = GetMarker();
                }

                await Task.Run(() => {
                    var req = new ListObjectsRequest(_ossConfig.BucketName)
                    {
                        Marker = marker,
                        //获取超过当前请求数量的数据，如果返回的比请求的多说明有下一页
                        MaxKeys = input.Size + 1,
                        Prefix = $"{_ossConfig.ObjectNamePrefix}/{input.ListPath}"
                    };
                    var listResult = _ossClient.ListObjects(req);
                    if (listResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var items = listResult.ObjectSummaries.ToList();
                        var returnItems = new List<ListItemOutput>();
                        returnItems.AddRange(items.Select(p => new ListItemOutput()
                        {
                            //拼接OSS可访问路径
                            Url = $"{(string.IsNullOrWhiteSpace(_ossConfig.CustomerDomain) ? $"{_ossConfig.BucketName}.{_ossConfig.EndPoint}/{p.Key}" : $"{_ossConfig.CustomerDomain}/{p.Key}")}",
                            Original = Path.GetFileName(p.Key),
                            Key = p.Key
                        }));
                        
                        output.Total = returnItems.Count;
                        if (returnItems.Count > input.Size)
                        {
                            output.Total = input.Start + input.Size + 1;
                            //获取请求数量的数据
                            returnItems = returnItems.GetRange(0, input.Size);
                            //获取下一页的分页标记
                            marker = returnItems.Last().Key;
                        }
                        else
                        {
                            //如果当前为最后一页则清除分页标记
                            marker = "";
                        }

                        output.List = returnItems;
                        output.State = "SUCCESS";

                        //保存分页标记用于请求后续数据
                        SetMarker(marker);
                    }
                    else
                    {
                        throw new UEditorServiceException("列出OSS文件失败");
                    }
                });
            }
            catch (Exception ex)
            {
                throw new UEditorServiceException("列出OSS文件操作失败", ex.Message);
            }

            return await Task.FromResult(output);
        }

        /// <summary>
        /// 获取OSS下一页开始的标记
        /// <para>此为默认实现，用Referer区分不同实例，但同一页面的不同实例（同一浏览器的不同选项卡）无法区分，如有需要可重写</para>
        /// </summary>
        /// <returns></returns>
        public virtual string GetMarker()
        {
            Context.Request.Cookies.TryGetValue(Context.Request.Headers["referer"], out string marker);
            return marker;
        }

        /// <summary>
        /// 保存OSS下一页开始的标记
        /// <para>此为默认实现，用Referer区分不同实例，但同一页面的不同实例（同一浏览器的不同选项卡）无法区分，如有需要可重写</para>
        /// </summary>
        /// <param name="marker"></param>
        public virtual void SetMarker(string marker)
        {
            Context.Response.Cookies.Append(Context.Request.Headers["referer"], marker);
        }
    }
}
