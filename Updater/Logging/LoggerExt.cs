using System;

namespace Updater.Logging
{
    public static class LoggerExt
    {
        public static string BuildExceptionMessage(this ILogger logger, Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
            {
                logException = x.InnerException;
            }

            string strErrorMsg = Environment.NewLine + "Message :" + logException.Message;

            strErrorMsg += Environment.NewLine + "Source :" + logException.Source;

            strErrorMsg += Environment.NewLine + "Stack Trace :" + logException.StackTrace;

            strErrorMsg += Environment.NewLine + "TargetSite :" + logException.TargetSite;
            return strErrorMsg;
        }
    }
}
