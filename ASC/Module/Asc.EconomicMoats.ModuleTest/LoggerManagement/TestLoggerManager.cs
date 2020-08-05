using EconomicMoats.Standard;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EconomicMoat.ModuleTest
{
    class TestLoggerManager
    {
        [Test]
        public void UC01_GetCurrentClassLogger()
        {
            LoggerManager LoggerMgr = new LoggerManager();
            Logger = LoggerMgr.GetCurrentClassLogger();
            WriteAllLevelLog();
        }

        [Test]
        public void UC02_GetMultipleLoggersByName()
        {
            LoggerManager LoggerMgr = new LoggerManager();
            Logger = LoggerMgr.GetLogger("Database.Connect");
            WriteAllLevelLog();
            Logger = LoggerMgr.GetLogger("Database.Query");
            WriteAllLevelLog();
            Logger = LoggerMgr.GetLogger("Database.SQL");
            WriteAllLevelLog();
        }

        private IStructuredLogger Logger;
        //private NLog.Logger Logger;
        private void WriteAllLevelLog()
        {
            Logger.Trace("This is Trace");
            Logger.Debug("This is Debug");
            Logger.Info("This is Information");
            Logger.Warn("This is Warning");
            Logger.Error("This is Error");
            Logger.Fatal("This is Fatal");
        }

        [Test]
        public void UC03_StructuredLogging_EventProperties()
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Logon by {user} from {ip_address}", "Kenny", "127.0.0.1");
            logger.Debug("{shopitem} added to basket by {user}",
                new { Id = 6, Name = "Jacket", Color = "Orange" }, "Kenny");
        }

        [Test]
        public void UC04_StructuredLogging_MessageFormatting()
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            Object o = null;

            /* Basic Message Formatting */
            logger.Info("Test {value1}", o); // null case. Result:  Test NULL
            logger.Info("Test {value1}", new DateTime(2018, 03, 25)); // datetime case. Result:  Test 25-3-2018 00:00:00 (locale TString)
            logger.Info("Test {value1}", new List<string> { "a", "b" }); // list of strings. Result: Test "a", "b"
            logger.Info("Test {value1}", new[] { "a", "b" }); // array. Result: Test "a", "b"
            logger.Info("Test {value1}", new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } }); // dict. Result:  Test "key1"=1, "key2"=2

            var order = new Order
            {
                OrderId = 2,
                Status = OrderStatus.Processing
            };

            /* Json Message Formatting */
            logger.Info("Test {value1}", order); // object Result: Test Name.Space.Order
            logger.Info("Test {@value1}", order); // object Result: Test {"OrderId":2, "Status":"Processing"}
            logger.Info("Test {value1}", new { OrderId = 2, Status = "Processing" }); // anomynous object. Result: Test { OrderId = 2, Status = Processing }
            logger.Info("Test {@value1}", new { OrderId = 2, Status = "Processing" }); // anomynous object. Result:Test {"OrderId":2, "Status":"Processing"}
        }

        public static class OrderStatus
        {
            public static string Processing = "Processing";
        };

        internal class Order
        {
            public int OrderId { get; set; }
            public string Status { get; set; }
        }

    }


}
