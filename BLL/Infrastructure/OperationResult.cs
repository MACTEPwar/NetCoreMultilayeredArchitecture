using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Infrastructure
{
    public class OperationResult<T>
    {
        public bool Successed { get; set; } = false;
        public int Datetime { get; private set; } = DateTime.Now.ToUnixTime();
        public string Property { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string InnerMessage { get; set; } = string.Empty;
        public T Value { get; set; } = default(T);

        public OperationResult(bool successed, T value = default(T), string message = "", string property = "", string innerMessage = "")
        {
            this.Successed = successed;
            this.Message = message;
            this.InnerMessage = innerMessage;
            this.Property = property;
            this.Value = value;
        }

        public static OperationResult<T> Success(T value = default(T), string message = "", string property = "")
        {
            return new OperationResult<T>(true, value, message, property);
        }

        public static OperationResult<T> Exception(string message = "", string property = "")
        {
            return new OperationResult<T>(false, default(T), message, property);
        }

        public static OperationResult<T> Exception(Exception exception)
        {
#if DEBUG
            var stackTrace = exception.StackTrace.Split("\n").ToList();
            stackTrace.Reverse();
            return new OperationResult<T>(false, default(T), exception.Message, String.Join("\n", stackTrace), exception.InnerException != null ? exception.InnerException.ToString() : string.Empty);
#endif
#if RELEASE
            string errorTrace = string.Empty;
            StackTrace stackTrace = new StackTrace(exception, true);
            if (stackTrace.FrameCount > 0)
            {
                errorTrace = "   Error ---> ";
                bool flag = false;
                for (int i = stackTrace.FrameCount - 1; i >= 0; i--)
                {
                    StackFrame frame = stackTrace.GetFrame(i);
                    int errorLine = frame.GetFileLineNumber();
                    string functionName = frame.GetMethod().Name;
                    Func<string> fff = () =>
                    {
                        flag = true;
                        return string.Format("{0} in ", exception.TargetSite.DeclaringType.FullName);
                    };
                    errorTrace += string.Format("{0}({2}line {1}).", functionName, errorLine, !flag ? fff() : "");
                }
            }
            return new OperationResult<T>(false, default(T), exception.Message, errorTrace, exception.InnerException != null ? exception.InnerException.ToString() : string.Empty);
#endif
        }

        public override string ToString()
        {
            if (!this.Successed)
            {
                return string.Format("Property:\n{0}\n ErrorMessage:\n   {1}{2}", !string.IsNullOrEmpty(this.Property) ? this.Property : string.Empty, !string.IsNullOrEmpty(this.Message) ? this.Message : string.Empty, !string.IsNullOrEmpty(this.InnerMessage) ? "\n InnerErrorMessage:\n   " + this.InnerMessage : string.Empty);
            }
            else
            {
                return string.Format("Property:\n   {0}\n Message:\n   {1}", !string.IsNullOrEmpty(this.Property) ? this.Property : string.Empty, !string.IsNullOrEmpty(this.Message) ? this.Message : string.Empty);
            }
        }
    }

    public class OperationResult
    {
        public bool Successed { get; set; } = false;
        public int Datetime { get; private set; } = DateTime.Now.ToUnixTime();
        public string Property { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string InnerMessage { get; set; } = string.Empty;

        public OperationResult(bool successed, string message = "", string property = "", string innerMessage = "")
        {
            this.Successed = successed;
            this.Message = message;
            this.InnerMessage = innerMessage;
            this.Property = property;
        }

        public static OperationResult Success(string message = "", string property = "")
        {
            return new OperationResult(true, message, property);
        }

        public static OperationResult Exception(string message = "", string property = "")
        {
            return new OperationResult(false, message, property);
        }

        public static OperationResult Exception(Exception exception)
        {
#if DEBUG
            var stackTrace = exception.StackTrace.Split("\n").ToList();
            stackTrace.Reverse();
            return new OperationResult(false, exception.Message, String.Join("\n", stackTrace), exception.InnerException != null ? exception.InnerException.ToString() : string.Empty);
#endif
#if RELEASE
            string errorTrace = string.Empty;
            StackTrace stackTrace = new StackTrace(exception, true);
            if (stackTrace.FrameCount > 0)
            {
                errorTrace = "   Error ---> ";
                bool flag = false;
                for (int i = stackTrace.FrameCount - 1; i >= 0; i--)
                {
                    StackFrame frame = stackTrace.GetFrame(i);
                    int errorLine = frame.GetFileLineNumber();
                    string functionName = frame.GetMethod().Name;
                    Func<string> fff = () =>
                    {
                        flag = true;
                        return string.Format("{0} in ", exception.TargetSite.DeclaringType.FullName);
                    };
                    errorTrace += string.Format("{0}({2}line {1}).", functionName, errorLine, !flag ? fff() : "");
                }
            }
            return new OperationResult<T>(false,exception.Message, errorTrace, exception.InnerException != null ? exception.InnerException.ToString() : string.Empty);
#endif
        }

        public override string ToString()
        {
            if (!this.Successed)
            {
                return string.Format("Property:\n{0}\n ErrorMessage:\n   {1}{2}", !string.IsNullOrEmpty(this.Property) ? this.Property : string.Empty, !string.IsNullOrEmpty(this.Message) ? this.Message : string.Empty, !string.IsNullOrEmpty(this.InnerMessage) ? "\n InnerErrorMessage:\n   " + this.InnerMessage : string.Empty);
            }
            else
            {
                return string.Format("Property:\n   {0}\n Message:\n   {1}", !string.IsNullOrEmpty(this.Property) ? this.Property : string.Empty, !string.IsNullOrEmpty(this.Message) ? this.Message : string.Empty);
            }
        }
    }

    public static class ExtentionOperationResult
    {
        //public static OperationResult<T> InLog<T>(this OperationResult<T> result, NLog.LogLevel level)
        //{
        //    var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    if (result.Successed)
        //    {
        //        logger.Log(level, result.ToString());
        //    }

        //    else
        //    {
        //        logger.Log(NLog.LogLevel.Error, result.ToString());
        //    }

        //    return result;
        //}
        //public static OperationResult InLog(this OperationResult result, NLog.LogLevel level)
        //{
        //    var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    if (result.Successed)
        //    {
        //        logger.Log(level, result.ToString());
        //    }

        //    else
        //    {
        //        logger.Log(NLog.LogLevel.Error, result.ToString());
        //    }

        //    return result;
        //}
    }
}
