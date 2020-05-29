# AspNetCore.UEditor

基于AspNetCore实现百度编辑器UEditor的后台服务，安装即用

目前支持上传/查看至网站指定目录和上传/查看至阿里云OSS

## 安装

##### 标准上传（上传至网站目录）：

	PM> Install-Package TxtName.AspNetCore.UEditor.Core -Version 1.0.0

##### 支持上传至阿里云OSS的版本：

	PM> Install-Package TxtName.AspNetCore.UEditor.AliyunOSS -Version 1.0.0
---
## NuGetPackage
Package  | NuGet 
-------- | :------------ 
TxtName.AspNetCore.UEditor.Core		| [![NuGet](https://img.shields.io/nuget/v/TxtName.AspNetCore.UEditor.Core.svg)](https://www.nuget.org/packages/TxtName.AspNetCore.UEditor.Core)
TxtName.AspNetCore.UEditor.AliyunOSS		| [![NuGet](https://img.shields.io/nuget/v/TxtName.AspNetCore.UEditor.AliyunOSS.svg)](https://www.nuget.org/packages/TxtName.AspNetCore.UEditor.AliyunOSS)
---

## 开发环境
* Windows 10
* Visual Studio 2019(16.4)
* .NET Core SDK 2.2
---

## 如何使用

##### 参照[Demo](https://github.com/txtname-cn/TxtName.AspNetCore.UEditor/tree/master/src/AspNetCore.UEditor.Demo)
##### 默认配置请求入口为"/api/UEditor"，可通过serviceConfig.EntryPath设置，但要注意同时修改ueditor.config.js里的serverUrl与EntryPath保持一致
##### 默认UEditor编辑器配置文件读取路径为"wwwroot/lib/ueditor/config.json"，可通过serviceConfig.ConfigFileName设置

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
        public string WebRootPath { get; set; }
        /// <summary>
        /// 服务列表
        /// </summary>
        internal List<UEditorRegisterService> RegisteredServices { get; set; }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //其他配置
    
        //参数可设置服务配置和UEditor配置
        services.AddUEditorService(serviceConfig=>{
            //开启阿里云OSS支持
            serviceConfig.UseAliyunOss();
        });
        
        //其他配置
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //其他配置
        
        //使用UEditorService中间件
        app.UseUEditorService();
        
        //其他配置
    }
---

# 其他
阿里云.NETSDK文档：
https://help.aliyun.com/document_detail/32085.html?spm=a2c4g.11186623.6.1154.7428d47erg0Adp

---
