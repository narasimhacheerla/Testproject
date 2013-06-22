using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using log4net;

namespace Snovaspace.Util.Logging
{
    public static class LoggingManager
    {
        public static ILog Logger = LogManager.GetLogger("LoggingManager");

        static LoggingManager()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Enter(int param = 0)
        {
            TraceIntoSystem(TraceEnterMessage(param));
            Logger.Debug(TraceEnterMessage(param));
        }

        private static void TraceIntoSystem(string message)
        {
            if (HttpContext.Current != null) HttpContext.Current.Trace.Write(message);
            Trace.Write(message);
        }

        public static void Debug(string message)
        {
            TraceIntoSystem(message);
            Logger.Debug(message);
        }

        public static void Info(string message)
        {
            TraceIntoSystem(message);
            Logger.Info(message);
        }

        public static void PrintObject<T>(T obj)
        {
            if (obj == null)
            {
                Logger.Info("Null value printed.");
                return;
            }

            string printOutput = obj.GetType().GetProperties().Aggregate(string.Empty, (current, property) =>
            {
                try
                {
                    return current + (property.Name + "=" + property.GetValue(obj, null)) + ",";
                }
                catch (Exception)
                {
                    return null;
                }
            });

            TraceIntoSystem(printOutput);
            Logger.Info(printOutput);
        }

        public static void PrintListObject<T>(IEnumerable<T> obj)
        {
            foreach (T innerObj in obj) PrintObject(innerObj);
        }

        public static void Error(Exception exception)
        {
            TraceIntoSystem("Error:" + exception.Message);
            Logger.Error(exception);
        }

        public static void Error(string message)
        {
            TraceIntoSystem(message);
            Logger.Error(message);
        }

        public static void Exit(int param = 0)
        {
            TraceIntoSystem(TraceExitMessage(param));
            Logger.Debug(TraceExitMessage(param));
        }

        /// <summary>
        /// Private struct used to obtain context information for the Logging routines.
        /// Provides easy access to information such as method name, namespace, source file
        /// name, etc.
        /// </summary>
        private struct ContextInformation
        {
            #region Constants

            private const bool CaptureFileInfo = true;            // Parameter to StackTrace constructor to make code more readable
            private const string DefaultText = "<unknown>";        // Default text for all of the properties
            private const int TargetStackFrame = 2;               // The StackFrame within StackTrace that contains the details I want

            #endregion

            private readonly string _namespace;
            private readonly string _className;
            private readonly string _methodName;
            private readonly string _methodArguments;

            public string Namespace
            {
                get { return _namespace; }
            }
            public string ClassName
            {
                get { return _className; }
            }
            public string MethodName
            {
                get { return _methodName; }
            }
            public string MethodArguments
            {
                get { return _methodArguments; }
            }

            public ContextInformation(bool dummyParam, int param)
            {
                // initialise private members
                _namespace = DefaultText;
                _className = DefaultText;
                _methodName = DefaultText;
                _methodArguments = DefaultText;

                try
                {
                    // Get a reference to the stack trace which will contain a number of stack frames:
                    //      frame(0) = this method, i.e. ContextInformation constructor
                    //      frame(1) = my caller, i.e. the trace method trying to get the context
                    //      frame(2) = the cap I actually want, i.e. the method which called Trace...().
                    int targetStackParam = TargetStackFrame + param;
                    var stack = new StackTrace(CaptureFileInfo);
                    if (stack.FrameCount >= targetStackParam + 1)
                    {
                        StackFrame targetStackFrame = stack.GetFrame(targetStackParam + 1);
                        MethodBase callingMethod = targetStackFrame.GetMethod();
                        _namespace = callingMethod.ReflectedType.Namespace;
                        _className = callingMethod.ReflectedType.Name;
                        _methodName = callingMethod.Name;
                        // Unfortunately we can only get argument name & type NOT value :-(
                        var methodArguments = new StringBuilder();
                        ParameterInfo[] methodParameters = callingMethod.GetParameters();
                        for (int parameterIndex = 0; parameterIndex < methodParameters.Length; parameterIndex++)
                        {
                            methodArguments.Append(methodParameters[parameterIndex].ParameterType.Name);
                            methodArguments.Append(" ");
                            methodArguments.Append(methodParameters[parameterIndex].Name);
                            if (parameterIndex < (methodParameters.Length - 1))
                                methodArguments.Append(", ");
                        }
                        _methodArguments = methodArguments.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Method to get Trace Mesage for Enter.
        /// </summary>
        private static string TraceEnterMessage(int param)
        {
            var contextInfo = new ContextInformation(true, param);
            string strMessage = "Enter " + contextInfo.Namespace + "." + contextInfo.ClassName + "." + contextInfo.MethodName + "(" + contextInfo.MethodArguments + ")";
            return strMessage;
        }

        /// <summary>
        /// Method to get Trace Mesage for Exit.
        /// </summary>
        private static string TraceExitMessage(int param = 0)
        {
            var contextInfo = new ContextInformation(true, param);
            string strMessage = "Exit " + contextInfo.Namespace + "." + contextInfo.ClassName + "." + contextInfo.MethodName + "(" + contextInfo.MethodArguments + ")";
            return strMessage;
        }

        public static void Debug(string[] messages)
        {
            if (messages != null)
                foreach (string message in messages)
                {
                    Debug(message);
                }
        }
    }
}


