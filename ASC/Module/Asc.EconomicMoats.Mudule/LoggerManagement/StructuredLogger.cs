using System;

namespace Asc
{
    public class StructuredLogger : IStructuredLogger
    {
        public NLog.Logger Logger;

        void IStructuredLogger.Debug(string v)
        {
            Logger.Debug("This is Debug");
        }

        void IStructuredLogger.Error(string v)
        {
            Logger.Error("This is Error");
        }

        void IStructuredLogger.Fatal(string v)
        {
            Logger.Fatal("This is Fatal");
        }

        void IStructuredLogger.Info(string v)
        {
            Logger.Info("This is Information");
        }

        void IStructuredLogger.Trace(string v)
        {
            Logger.Trace("This is Verbose");
        }

        void IStructuredLogger.Warn(string v)
        {
            Logger.Warn("This is Warning");
        }
    }
}
