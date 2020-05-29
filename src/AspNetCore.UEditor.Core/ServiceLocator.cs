using Microsoft.Extensions.DependencyInjection;
using System;

namespace TxtName.AspNetCore.UEditor.Core
{
    /// <summary>
    /// 用于在无法正常获得<see cref="IServiceCollection"/>和<see cref="IServiceProvider"/>的时候使用
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// IServiceProvider
        /// </summary>
        public static IServiceProvider ServiceProvider;
        /// <summary>
        /// IServiceCollection
        /// </summary>
        public static IServiceCollection ServiceCollection;
    }
}
