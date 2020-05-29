using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TxtName.AspNetCore.UEditor.Core.Services.Lists
{
    /// <summary>
    /// UEditor列出文件服务
    /// <para>此为默认实现，如有需要可重写</para>
    /// </summary>
    public class UEditorListService : UEditorService, IUEditorListService
    {

        /// <summary>
        /// 列出文件
        /// </summary>
        /// <returns></returns>
        public async Task<ListOutput> ListAsync()
        {
            var input = await GetInputParamAsync();
            return await ListCoreAsync(input);
        }

        /// <summary>
        /// 封装列出文件需要的参数
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ListInput> GetInputParamAsync()
        {
            var input = new ListInput()
            {
                Start = Convert.ToInt32(Context.Request.Query["start"]),
                Size = Convert.ToInt32(Context.Request.Query["size"])
            };

            switch (Action)
            {
                case "listimage":
                    input.ListPath = UEditorConfig.ImageManagerListPath;
                    input.AllowFileExtensions = UEditorConfig.ImageManagerAllowFiles;
                    break;
                case "listfile":
                    input.ListPath = UEditorConfig.FileManagerListPath;
                    input.AllowFileExtensions = UEditorConfig.FileManagerAllowFiles;
                    break;
            }

            //处理列出目录的路径，移除路径前的“/”以免造成后续路径拼接出错
            if (input.ListPath.StartsWith("/"))
            {
                input.ListPath = input.ListPath.Substring(1, input.ListPath.Length - 1);
            }

            return await Task.FromResult(input);
        }

        /// <summary>
        /// 列出文件的具体实现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<ListOutput> ListCoreAsync(ListInput input)
        {
            //要列出的绝对路径
            var absolutePath = Path.Combine($"{ServiceConfig.WebRootPath}\\", $"{input.ListPath}");
            if (!Directory.Exists(absolutePath))
            {
                return new ListOutput() { State = "目录不存在" };
            }

            var allFiles = new string[10];
            //根据允许列出的文件类型列表过滤
            var files = Directory.GetFiles(absolutePath, "*.*", SearchOption.AllDirectories)
                .Where(p=>input.AllowFileExtensions.Contains(Path.GetExtension(p)))
                .Select(p=>Path.GetRelativePath(ServiceConfig.WebRootPath,p)).ToList();

            //分页
            var pagedList = files.Count > input.Start + input.Size ? files.GetRange(input.Start, input.Size) : files.GetRange(input.Start,files.Count - input.Start);
            return await Task.FromResult(new ListOutput() { 
                Size = input.Size
                ,Total = files.Count
                ,Start = input.Start
                ,List = pagedList.Select(p=>new ListItemOutput() { Url = p.Replace("\\", "/"),Original = Path.GetFileName(p) }).ToList()
                ,State = "SUCCESS"
            });
        }
    }
}
