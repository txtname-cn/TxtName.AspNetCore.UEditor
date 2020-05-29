using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TxtName.AspNetCore.UEditor.Core.Services.Uploads
{
    /// <summary>
    /// UEditor上传文件服务
    /// <para>此为默认实现，如有需要可重写</para>
    /// </summary>
    public class UEditorUploadService : UEditorService,IUEditorUploadService
    {

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public async Task<UploadOutput> UploadAsync()
        {
            var input = await GetUploadParamAsync();
            return await UploadCoreAsync(input);
        }

        /// <summary>
        /// 封装上传文件需要的参数
        /// </summary>
        /// <returns></returns>
        public async Task<UploadCoreInput> GetUploadParamAsync()
        {
            UploadCoreInput input = new UploadCoreInput();
            var uploadFieldName = "";
            var allowExtensionList = new List<string>();
            var allowSize = 0;
            var format = "";
            var urlPrefix = "";

            //涂鸦和截图均为Base64数据上传
            var isBase64Content = "uploadscrawl".Equals(Action);

            if (isBase64Content)
            {
                input.FileName = "scrawl.png";
                input.FileBytes = Convert.FromBase64String(Context.Request.Form[UEditorConfig.ScrawlFieldName]);
                if (input.FileBytes.Length > UEditorConfig.ScrawlMaxSize)
                {
                    throw new UEditorServiceException($"文件大小超出限制");
                }
                format = UEditorConfig.ScrawlPathFormat;
                urlPrefix = UEditorConfig.ScrawlUrlPrefix;
            }
            else
            {
                //取得公共参数
                switch (Action)
                {
                    case "uploadimage":
                        uploadFieldName = UEditorConfig.ImageFieldName;
                        allowExtensionList = UEditorConfig.ImageAllowFiles;
                        allowSize = UEditorConfig.ImageMaxSize;
                        format = UEditorConfig.ImagePathFormat;
                        urlPrefix = UEditorConfig.ImageUrlPrefix;
                        break;
                    case "uploadvideo":
                        uploadFieldName = UEditorConfig.VideoFieldName;
                        allowExtensionList = UEditorConfig.VideoAllowFiles;
                        allowSize = UEditorConfig.VideoMaxSize;
                        format = UEditorConfig.VideoPathFormat;
                        urlPrefix = UEditorConfig.VideoUrlPrefix;
                        break;
                    case "uploadfile":
                        uploadFieldName = UEditorConfig.FileFieldName;
                        allowExtensionList = UEditorConfig.FileAllowFiles;
                        allowSize = UEditorConfig.FileMaxSize;
                        format = UEditorConfig.FilePathFormat;
                        urlPrefix = UEditorConfig.FileUrlPrefix;
                        break;
                }

                var file = Context.Request.Form.Files[uploadFieldName];
                input.OriginalFileName = file.FileName;
                input.FileName = file.FileName;
                if (!allowExtensionList.Select(p => p.ToLower()).Contains(Path.GetExtension(file.FileName)))
                {
                    throw new UEditorServiceException($"文件格式被禁止上传");
                }
                if (file.Length > allowSize)
                {
                    throw new UEditorServiceException($"文件大小超出限制");
                }

                try
                {
                    var fileBytes = new byte[file.Length];
                    await file.OpenReadStream().ReadAsync(fileBytes, 0, fileBytes.Length);
                    input.FileBytes = fileBytes;
                }
                catch (Exception ex)
                {
                    throw new UEditorServiceException($"网络错误",ex.Message);
                }
            }

            input.UrlPrefix = urlPrefix;
            input.FileName = UEditorPathFormat(input.FileName, format);

            //处理上传路径，移除路径前的“/”以免造成后续路径拼接出错
            if (input.UrlPrefix.StartsWith("/"))
            {
                input.UrlPrefix = input.UrlPrefix.Substring(1, input.UrlPrefix.Length - 1);
            }
            //在路径结尾添加“/”以免造成后续路径拼接出错
            if (input.UrlPrefix.Length > 1 && !input.UrlPrefix.EndsWith("/"))
            {
                input.UrlPrefix += "/";
            }

            return await Task.FromResult(input);
        }

        /// <summary>
        /// 上传文件的具体实现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<UploadOutput> UploadCoreAsync(UploadCoreInput input)
        {
            var output = new UploadOutput();

            try
            {
                //文件保存的绝对路径
                var absolutePath = Path.Combine($"{ServiceConfig.WebRootPath}\\", $"{input.UrlPrefix}{input.FileName}");

                if (!Directory.Exists(Path.GetDirectoryName(absolutePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
                }

                File.WriteAllBytes(absolutePath, input.FileBytes);

                output.State = "SUCCESS";
                output.Original = input.OriginalFileName;
                output.Url = Path.GetRelativePath(Path.Combine($"{ServiceConfig.WebRootPath}\\",input.UrlPrefix),absolutePath).Replace("\\","/");
            }
            catch (Exception ex)
            {
                throw new UEditorServiceException("文件访问错误", ex.Message);
            }

            return await Task.FromResult(output);
        }

        /// <summary>
        /// URL格式化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected virtual string UEditorPathFormat(string fileName, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "{yyyy}{mm}{dd}{hh}{ii}{ss}{rand:6}";
            }

            string tmpFileName = fileName;
            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            tmpFileName = invalidPattern.Replace(tmpFileName, "");

            string ext = Path.GetExtension(tmpFileName);
            string name = Path.GetFileNameWithoutExtension(tmpFileName);

            format = format.Replace("{filename}", name);
            format = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(format, new MatchEvaluator(delegate (Match match)
            {
                var digit = 6;
                if (match.Groups.Count > 2)
                {
                    digit = Convert.ToInt32(match.Groups[2].Value);
                }
                var rand = new Random();
                return rand.Next((int)Math.Pow(10, digit), (int)Math.Pow(10, digit + 1)).ToString();
            }));

            format = format.Replace("{time}", DateTime.Now.Ticks.ToString());
            format = format.Replace("{yyyy}", DateTime.Now.Year.ToString());
            format = format.Replace("{yy}", (DateTime.Now.Year % 100).ToString("D2"));
            format = format.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
            format = format.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
            format = format.Replace("{hh}", DateTime.Now.Hour.ToString("D2"));
            format = format.Replace("{ii}", DateTime.Now.Minute.ToString("D2"));
            format = format.Replace("{ss}", DateTime.Now.Second.ToString("D2"));

            return format + ext;
        }
    }
}
