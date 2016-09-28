namespace Logging
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public interface ILogSink
    {
        void Log(string message);
    }

    public static class Log
    {
        private static Stopwatch MasterWatch;

        public static ILogSink LogSink
        {
            get;
            set;
        }

        public static void Info(string message)
        {
            LogSink.Log("[INFO] " + message);
        }

        public static void Info(string format, params object[] args)
        {
            LogSink.Log("[INFO] " + string.Format(format, args));
        }

        public static PerfBlock Perf(string message)
        {
            return new PerfBlock(message);
        }

        public static PerfBlock Perf(string format, params object[] args)
        {
            return new PerfBlock(string.Format(format, args));
        }

        public static void StartMasterWatch()
        {
            MasterWatch = Stopwatch.StartNew();
        }

        public static void StoppMasterWatch(string message)
        {
            MasterWatch.Stop();
            Log.LogSink.Log($"[PERF] ************** {message} [{MasterWatch.ElapsedMilliseconds}ms ************]");
        }

        public struct PerfBlock : IDisposable
        {
            private static int nextLevel;
            private readonly string message;
            private readonly Stopwatch stopwatch;
            private readonly int level;

            public PerfBlock(string message)
            {
                this.message = message;
                this.stopwatch = Stopwatch.StartNew();
                this.level = Interlocked.Increment(ref nextLevel);
            }

            public void Dispose()
            {
                this.stopwatch.Stop();
                Log.LogSink.Log($"[PERF] {new string(' ', this.level * 4)} {this.message} [{this.stopwatch.ElapsedMilliseconds}ms]");
                Interlocked.Decrement(ref nextLevel);
            }
        }
    }
}