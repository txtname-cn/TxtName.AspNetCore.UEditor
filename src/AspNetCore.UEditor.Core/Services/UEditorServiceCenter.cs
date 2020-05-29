using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TxtName.AspNetCore.UEditor.Core.Services.Uploads;
using TxtName.AspNetCore.UEditor.Core.Services.Configs;
using TxtName.AspNetCore.UEditor.Core.Services.Lists;

namespace TxtName.AspNetCore.UEditor.Core.Services
{
    /// <summary>
    /// UEditor服务中心，服务统一入口
    /// </summary>
    public class UEditorServiceCenter
    {
        private readonly IUEditorConfigService _configService;
        private readonly IUEditorUploadService _uploadService;
        private readonly IUEditorListService _listService;

        private readonly string _action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configService"></param>
        /// <param name="uploadService"></param>
        /// <param name="listService"></param>
        public UEditorServiceCenter(IHttpContextAccessor httpContextAccessor,IUEditorConfigService configService, IUEditorUploadService uploadService,IUEditorListService listService)
        {
            _configService = configService;
            _uploadService = uploadService;
            _listService = listService;
            _action = httpContextAccessor.HttpContext.Request.Query["action"].ToString().ToLower();
        }

        /// <summary>
        /// 服务统一入口
        /// </summary>
        /// <returns></returns>
        public async Task<object> DoActionAsync()
        {
            switch (_action)
            {
                case "config":
                    return await _configService.GetConfigAsync();
                case "uploadimage":
                case "uploadscrawl":
                case "uploadvideo":
                case "uploadfile":
                    return await _uploadService.UploadAsync();
                case "listimage":
                case "listfile":
                    return await _listService.ListAsync();
                case "reload":
                    await _configService.ReloadConfigAsync();
                    return new UEditorOutput() { State = "SUCCESS" };
                default:
                    throw new UEditorServiceException($"不支持的操作:{_action}");
            }            
        }
    }
}
