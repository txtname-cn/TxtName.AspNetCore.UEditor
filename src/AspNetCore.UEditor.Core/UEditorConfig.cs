using System.Collections.Generic;

namespace TxtName.AspNetCore.UEditor.Core
{
    /// <summary>
    /// 编辑器配置文件
    /// </summary>
    public class UEditorConfig
    {
        #region 上传图片配置项
        /// <summary>
        /// 执行上传图片的action名称
        /// </summary>
        public string ImageActionName { get; set; }
        /// <summary>
        /// 提交的图片表单名称
        /// </summary>
        public string ImageFieldName { get; set; }
        /// <summary>
        /// 上传大小限制，单位B
        /// </summary>
        public int ImageMaxSize { get; set; }
        /// <summary>
        /// 上传图片格式显示
        /// </summary>
        public List<string> ImageAllowFiles { get; set; }
        /// <summary>
        /// 是否压缩图片
        /// </summary>
        public bool ImageCompressEnable { get; set; }
        /// <summary>
        /// 图片压缩最长边限制
        /// </summary>
        public int ImageCompressBorder { get; set; }
        /// <summary>
        /// 插入的图片浮动方式
        /// </summary>
        public string ImageInsertAlign { get; set; }
        /// <summary>
        /// 图片访问路径前缀
        /// </summary>
        public string ImageUrlPrefix { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string ImagePathFormat { get; set; }
        #endregion

        #region 涂鸦图片上传配置项
        /// <summary>
        /// 执行上传涂鸦的action名称
        /// </summary>
        public string ScrawlActionName { get; set; }
        /// <summary>
        /// 提交的图片表单名称
        /// </summary>
        public string ScrawlFieldName { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string ScrawlPathFormat { get; set; }
        /// <summary>
        /// 上传大小限制，单位B
        /// </summary>
        public int ScrawlMaxSize { get; set; }
        /// <summary>
        /// 图片访问路径前缀
        /// </summary>
        public string ScrawlUrlPrefix { get; set; }
        /// <summary>
        /// 插入的图片浮动方式
        /// </summary>
        public string ScrawlInsertAlign { get; set; }
        #endregion

        #region 截图工具上传配置项
        /// <summary>
        /// 执行上传截图的action名称
        /// </summary>
        public string SnapscreenActionName { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string SnapscreenPathFormat { get; set; }
        /// <summary>
        /// 图片访问路径前缀
        /// </summary>
        public string SnapscreenUrlPrefix { get; set; }
        /// <summary>
        /// 插入的图片浮动方式
        /// </summary>
        public string SnapscreenInsertAlign { get; set; }
        #endregion

        #region 抓取远程图片配置项
        /// <summary>
        /// 
        /// </summary>
        public List<string> CatcherLocalDomain { get; set; }
        /// <summary>
        /// 执行抓取远程图片的action名称
        /// </summary>
        public string CatcherActionName { get; set; }
        /// <summary>
        /// 提交的图片列表表单名称
        /// </summary>
        public string CatcherFieldName { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string CatcherPathFormat { get; set; }
        /// <summary>
        /// 图片访问路径前缀
        /// </summary>
        public string CatcherUrlPrefix { get; set; }
        /// <summary>
        /// 抓取图片格式显示
        /// </summary>
        public List<string> CatcherAllowFiles { get; set; }
        /// <summary>
        /// 上传大小限制，单位B
        /// </summary>
        public int CatcherMaxSize { get; set; }
        #endregion

        #region 上传视频配置项
        /// <summary>
        /// 执行上传视频的action名称
        /// </summary>
        public string VideoActionName { get; set; }
        /// <summary>
        /// 提交的视频表单名称
        /// </summary>
        public string VideoFieldName { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string VideoPathFormat { get; set; }
        /// <summary>
        /// 视频访问路径前缀
        /// </summary>
        public string VideoUrlPrefix { get; set; }
        /// <summary>
        /// 上传大小限制，单位B，默认100MB
        /// </summary>
        public int VideoMaxSize { get; set; }
        /// <summary>
        /// 上传视频格式显示
        /// </summary>
        public List<string> VideoAllowFiles { get; set; }
        #endregion

        #region 上传文件配置
        /// <summary>
        /// 执行上传视频的action名称
        /// </summary>
        public string FileActionName { get; set; }
        /// <summary>
        /// 提交的文件表单名称
        /// </summary>
        public string FileFieldName { get; set; }
        /// <summary>
        /// 上传保存路径,可以自定义保存路径和文件名格式
        /// </summary>
        public string FilePathFormat { get; set; }
        /// <summary>
        /// 文件访问路径前缀
        /// </summary>
        public string FileUrlPrefix { get; set; }
        /// <summary>
        /// 上传大小限制，单位B，默认50MB
        /// </summary>
        public int FileMaxSize { get; set; }
        /// <summary>
        /// 上传文件格式显示
        /// </summary>
        public List<string> FileAllowFiles { get; set; }
        #endregion

        #region 列出指定目录下的图片
        /// <summary>
        /// 执行图片管理的action名称
        /// </summary>
        public string ImageManagerActionName { get; set; }
        /// <summary>
        /// 指定要列出图片的目录
        /// </summary>
        public string ImageManagerListPath { get; set; }
        /// <summary>
        /// 每次列出文件数量
        /// </summary>
        public int ImageManagerListSize { get; set; }
        /// <summary>
        /// 图片访问路径前缀
        /// </summary>
        public string ImageManagerUrlPrefix { get; set; }
        /// <summary>
        /// 插入的图片浮动方式
        /// </summary>
        public string ImageManagerInsertAlign { get; set; }
        /// <summary>
        /// 列出的文件类型
        /// </summary>
        public List<string> ImageManagerAllowFiles { get; set; }
        #endregion

        #region 列出指定目录下的文件
        /// <summary>
        /// 执行文件管理的action名称
        /// </summary>
        public string FileManagerActionName { get; set; }
        /// <summary>
        /// 指定要列出文件的目录
        /// </summary>
        public string FileManagerListPath { get; set; }
        /// <summary>
        /// 文件访问路径前缀
        /// </summary>
        public string FileManagerUrlPrefix { get; set; }
        /// <summary>
        /// 每次列出文件数量
        /// </summary>
        public int FileManagerListSize { get; set; }
        /// <summary>
        /// 列出的文件类型
        /// </summary>
        public List<string> FileManagerAllowFiles { get; set; }
        #endregion
    }
}
