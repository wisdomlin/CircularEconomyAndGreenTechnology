using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{
    public class LoggerManager
    {
        public LoggerManager()
        {
        }

        public IStructuredLogger GetLogger(string LoggerName)
        {
            StructuredLogger l = new StructuredLogger();
            l.Logger = NLog.LogManager.GetLogger(LoggerName);
            return l;
        }

        public IStructuredLogger GetCurrentClassLogger()
        {
            StructuredLogger l = new StructuredLogger();
            l.Logger = NLog.LogManager.GetCurrentClassLogger();
            return l;
        }
    }
}
