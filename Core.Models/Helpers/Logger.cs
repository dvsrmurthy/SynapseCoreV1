using System.Linq;
using System;
using log4net.Appender;

namespace Core.Models.Helpers
{
    public static class Logger
    {
        private static log4net.ILog Log { get; set; }

        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        public static void Debug(object msg)
        {
            Log.Debug(msg);
        }

        public static void Debug(object msg, Exception e)
        {
            Log.Debug(msg, e);
        }

        public static void Debug(Exception e)
        {
            Log.Debug(e.Message, e);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        public static void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.DebugFormat(provider, format, args);
        }

        public static void DebugFormat(string format, object arg0, object arg1)
        {
            Log.DebugFormat(format, arg0, arg1);
        }

        public static void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.DebugFormat(format, arg0, arg1, arg2);
        }

        public static void Fatal(Exception e)
        {
            Log.Fatal(e.Message, e);
        }

        public static void Fatal(object msg)
        {
            Log.Fatal(msg);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            Log.FatalFormat(format, args);
        }

        public static void SetLoggerPath(string logFilename)
        {
            var repository = log4net.LogManager.GetRepository();
            foreach (
                var fileAppender in
                    repository.GetAppenders()
                        .Where(
                            appender =>
                                String.Compare(appender.Name, "LogFileAppender", StringComparison.OrdinalIgnoreCase) == 0 &&
                                appender is FileAppender)
                        .Cast<FileAppender>())
            {
                fileAppender.File = System.IO.Path.Combine("../logs/", "log_" + logFilename + ".txt");
                fileAppender.ActivateOptions();
            }
        }

        public static void LogSqlQueries(string sql)
        {
            Log.DebugFormat(sql);
        }
    }
}
