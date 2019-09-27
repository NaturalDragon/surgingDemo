using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Log
{
    public class Logger
    {
        private static Logger _instance;

        private static ILog _log;

        public static void GetLogger(string repository, string name)
        {
            _log = Provider.GetLogger(repository, name);
        }

        public static ILoggerRepository CreateLogger(string repository)
        {
            return Provider.RegistRepository(repository);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="description">错误描述</param>
        public static void Error(string description)
        {
            var describes = BuildInfoDescription(description);
            _log.Error(describes);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="description">错误描述</param>
        /// <param name="exception"></param>
        public static void Error(string description, Exception exception)
        {
            var describes = BuildInfoDescription(description);
            _log.Error(describes, exception);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Error(Exception ex)
        {
            var describes = BuildErrorDescription(ex);
            _log.Error(describes);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="describes">信息描述</param>
        public static void Info(string describes)
        {
            describes = BuildInfoDescription(describes);
            _log.Info(describes);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="describes">调试输出信息</param>
        public static void Debug(string describes)
        {
            describes = BuildDescription(describes);
            _log.Debug(describes);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="describes">调试输出信息</param>
        /// <param name="exception"></param>
        public static void Debug(string describes, Exception exception)
        {
            describes = BuildDescription(describes);
            _log.Debug(describes, exception);
        }

        static string BuildInfoDescription(string describes)
        {
            var message = string.Format("[描述]：{0}", describes);
            return BuildDescription(message);
        }

        static string BuildErrorDescription(Exception ex)
        {
            var errorMessage = string.Format("[描述]：{0} {1}", ex.Message, ex.StackTrace);
            return BuildDescription(errorMessage);
        }

        /// <summary>
        /// 异常输入内容构建
        /// </summary>
        /// <param name="describes">异常描述</param>
        /// <param name="ex">异常对象</param>
        /// <returns></returns>
        static string BuildDescription(string describes)
        {

            var stackTrace = new System.Diagnostics.StackTrace(1, true);
            var fileName = stackTrace.GetFrame(1).GetFileName();
            var lineNumber = stackTrace.GetFrame(1).GetFileLineNumber();

            var builder = new StringBuilder();
            builder.Append(string.Format("[时间]：{0} \r\n", DateTime.Now.ToString()));
            builder.Append(string.Format("[文件]：{0} \r\n", fileName));
            builder.Append(string.Format("[行号]：{0} \r\n", lineNumber));
            builder.Append(describes);
            builder.Append("\r\n");

            return builder.ToString();
        }
    }
}
