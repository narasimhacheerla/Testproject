//using System;
//using System.Diagnostics;
//using PostSharp.Aspects;
//using Snovaspace.Util.Logging;

//namespace Snovaspace.Util.PostSharp
//{
//    [Serializable]
//    [ProfilerAspect(AttributeExclude = true)]
//    public class ProfilerAspect : OnMethodBoundaryAspect
//    {
//        public override void OnEntry(MethodExecutionArgs args)
//        {
//            args.MethodExecutionTag = Stopwatch.StartNew();

//            if (args.Method.DeclaringType != null)
//                LoggingManager.Debug("Entry " + args.Method.DeclaringType.Namespace + " " + args.Method.DeclaringType.Name + " " + args.Method.Name);
//        }

//        public override void OnExit(MethodExecutionArgs args)
//        {
//            if (args.Method.DeclaringType != null)
//                LoggingManager.Debug("Exit " + args.Method.DeclaringType.Namespace + " " + args.Method.DeclaringType.Name + " " + args.Method.Name);

//            var sw = (Stopwatch)args.MethodExecutionTag;
//            sw.Stop();
//            if (args.Method.DeclaringType != null)
//            {
//                if (sw.Elapsed.TotalSeconds > 1)
//                {
//                    string output = string.Format("Alert: {0} - {1} Executed in {2} seconds", args.Method.DeclaringType.Name, args.Method.Name, sw.ElapsedMilliseconds / 1000);
//                    LoggingManager.Debug(output);
//                }
//            }
//        }
//    }
//}