using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TxtName.AspNetCore.UEditor.Core.Middlewares;
using TxtName.AspNetCore.UEditor.Core.Services;
using TxtName.AspNetCore.UEditor.Core.Services.Configs;
using TxtName.AspNetCore.UEditor.Core.Services.Lists;
using TxtName.AspNetCore.UEditor.Core.Services.Uploads;

namespace TxtName.AspNetCore.UEditor.Core
{
    /// <summary>
    /// 注册UEditor服务
    /// </summary>
    public static class UEditorAspNetCoreServiceExtensions
    {
        /// <summary>
        /// 注册UEditor服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddUEditorService(this IServiceCollection services)
        {
            services.AddUEditorService(serviceConfig => { },ueditorConfig=> { });
        }

        /// <summary>
        /// 注册UEditor服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfig">UEditor服务配置</param>
        /// <returns></returns>
        public static void AddUEditorService(this IServiceCollection services, Action<UEditorServiceConfig> serviceConfig)
        {
            services.AddUEditorService(serviceConfig, ueditorConfig=> { });
        }

        /// <summary>
        /// 注册UEditor服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ueditorConfig">UEditor编辑器配置</param>
        /// <returns></returns>
        public static void AddUEditorService(this IServiceCollection services,Action<UEditorConfig> ueditorConfig)
        {
            services.AddUEditorService(serviceConfig => { }, ueditorConfig);
        }

        /// <summary>
        /// 注册UEditor服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfig">UEditor服务配置</param>
        /// <param name="ueditorConfig">UEditor编辑器配置</param>
        /// <returns></returns>
        public static void AddUEditorService(this IServiceCollection services, Action<UEditorServiceConfig> serviceConfig,Action<UEditorConfig> ueditorConfig)
        {
            ServiceLocator.ServiceCollection = services;

            //添加服务集合
            var UEditorServiceConfig = new UEditorServiceConfig
            {
                RegisteredServices = new List<UEditorRegisterService> {
                    new UEditorRegisterService() { Service = typeof(IUEditorConfigService), Implmentation = typeof(UEditorConfigService) },
                    new UEditorRegisterService() { Service = typeof(IUEditorUploadService),Implmentation = typeof(UEditorUploadService) },
                    new UEditorRegisterService() { Service = typeof(IUEditorListService),Implmentation = typeof(UEditorListService) }
                }
            };
            //提供用户修改服务配置和注册服务
            serviceConfig.Invoke(UEditorServiceConfig);

            if (!File.Exists(UEditorServiceConfig.ConfigFileName))
            {
                throw new FileNotFoundException("没有找到UEditor配置文件，请确认文件位置和配置路径", UEditorServiceConfig.ConfigFileName);
            }

            var ueditorConfigFromFile = JsonConvert.DeserializeObject<UEditorConfig>(File.ReadAllText(UEditorServiceConfig.ConfigFileName, Encoding.UTF8));
            //提供用户修改编辑器配置，此处设置的配置会覆盖文件配置
            ueditorConfig.Invoke(ueditorConfigFromFile);

            services.AddSingleton(ueditorConfigFromFile);
            services.AddSingleton(UEditorServiceConfig);

            services.AddHttpContextAccessor();

            //注入UEditor服务的默认实现
            UEditorServiceConfig.RegisteredServices.ForEach(p => {
                services.AddScoped(p.Service, p.Implmentation);
            });
            //注册UEditor服务中心
            services.AddScoped<UEditorServiceCenter>();
        }

        /// <summary>
        /// 使用UEditor服务中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUEditorService(this IApplicationBuilder app)
        {
            ServiceLocator.ServiceProvider = app.ApplicationServices;

            var UEditorServiceConfig = app.ApplicationServices.GetRequiredService<UEditorServiceConfig>();
            //设置Web根目录
            UEditorServiceConfig.WebRootPath = app.ApplicationServices.GetRequiredService<IHostingEnvironment>().WebRootPath;
            //设置UEditor服务的请求处理路径
            app.UseWhen(context => context.Request.Path.Value.ToLower().StartsWith(UEditorServiceConfig.EntryPath.ToLower()), config =>
            {
                config.UseMiddleware<UEditorMiddleware>();
            });

            return app;
        }

        /// <summary>
        /// 注册/替换UEditor服务
        /// <para>当有多个类实现同一服务时以最后一次注册为准</para>
        /// </summary>
        /// <typeparam name="TService">UEditor服务</typeparam>
        /// <typeparam name="TImplementation">UEditor服务的实现</typeparam>
        /// <param name="serviceConfig"></param>
        public static void RegisterService<TService,TImplementation>(this UEditorServiceConfig serviceConfig) where TService:IUEditorService where TImplementation : TService
        {
            var existsService = serviceConfig.RegisteredServices.Find(p=>p.Service == typeof(TService));
            if (existsService != null)
            {
                existsService.Implmentation = typeof(TImplementation);
                return;
            }

            serviceConfig.RegisteredServices.Add(new UEditorRegisterService() { 
                Service = typeof(TService)
                ,Implmentation = typeof(TImplementation)
            });
        }
    }

    /// <summary>
    /// UEditor服务对象
    /// </summary>
    public class UEditorRegisterService
    {
        /// <summary>
        /// UEditor服务
        /// </summary>
        public Type Service { get; set; }
        /// <summary>
        /// UEditor服务的实现
        /// </summary>
        public Type Implmentation { get; set; }
    }
}
