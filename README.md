# AspNetCore.UEditor

����AspNetCoreʵ�ְٶȱ༭��UEditor�ĺ�̨���񣬰�װ����

Ŀǰ֧���ϴ�/�鿴����վָ��Ŀ¼���ϴ�/�鿴��������OSS

## ��װ

##### ��׼�ϴ����ϴ�����վĿ¼����

	PM> Install-Package TxtName.AspNetCore.UEditor.Core -Version 1.0.0

##### ֧���ϴ���������OSS�İ汾��

	PM> Install-Package TxtName.AspNetCore.UEditor.AliyunOSS -Version 1.0.0
---
## NuGetPackage
Package  | NuGet 
-------- | :------------ 
TxtName.AspNetCore.UEditor.Core		| [![NuGet](https://img.shields.io/nuget/v/TxtName.AspNetCore.UEditor.Core.svg)](https://www.nuget.org/packages/TxtName.AspNetCore.UEditor.Core)
TxtName.AspNetCore.UEditor.AliyunOSS		| [![NuGet](https://img.shields.io/nuget/v/TxtName.AspNetCore.UEditor.AliyunOSS.svg)](https://www.nuget.org/packages/TxtName.AspNetCore.UEditor.AliyunOSS)
---

## ��������
* Windows 10
* Visual Studio 2019(16.4)
* .NET Core SDK 2.2
---

## ���ʹ��

##### ����[Demo](https://github.com/txtname-cn/TxtName.AspNetCore.UEditor/tree/master/src/AspNetCore.UEditor.Demo)
##### Ĭ�������������Ϊ"/api/UEditor"����ͨ��serviceConfig.EntryPath���ã���Ҫע��ͬʱ�޸�ueditor.config.js���serverUrl��EntryPath����һ��
##### Ĭ��UEditor�༭�������ļ���ȡ·��Ϊ"wwwroot/lib/ueditor/config.json"����ͨ��serviceConfig.ConfigFileName����

    /// <summary>
    /// ��������
    /// </summary>
    public class UEditorServiceConfig
    {
        /// <summary>
        /// UEditor��������·����Ĭ��Ϊ /api/ueditor
        /// </summary>
        public string EntryPath { get; set; } = "/api/ueditor";
        /// <summary>
        /// UEditor�����ļ�·����Ĭ��Ϊ wwwroot/lib/ueditor/config.json
        /// </summary>
        public string ConfigFileName { get; set; } = $"wwwroot/lib/ueditor/config.json";
        /// <summary>
        /// WebRootPath
        /// </summary>
        public string WebRootPath { get; set; }
        /// <summary>
        /// �����б�
        /// </summary>
        internal List<UEditorRegisterService> RegisteredServices { get; set; }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //��������
    
        //���������÷������ú�UEditor����
        services.AddUEditorService(serviceConfig=>{
            //����������OSS֧��
            serviceConfig.UseAliyunOss();
        });
        
        //��������
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //��������
        
        //ʹ��UEditorService�м��
        app.UseUEditorService();
        
        //��������
    }
---

# ����
������.NETSDK�ĵ���
https://help.aliyun.com/document_detail/32085.html?spm=a2c4g.11186623.6.1154.7428d47erg0Adp

---
