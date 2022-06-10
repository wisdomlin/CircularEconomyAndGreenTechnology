using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace XXXEntity
{
    public class CodeTimer : IDisposable
    {
        private Stopwatch stopwatch = new Stopwatch();
        private Action<TimeSpan> reportFunction;

        /// <summary>
        /// constructor overloading
        /// TimeSpan ellapsed is a report function.
        /// report function could be defined from outside.
        /// </summary>
        /// <param name="name"></param>
        public CodeTimer(string name) : this((TimeSpan ellapsed) =>
        {
            Console.WriteLine($"{name} took {ellapsed.TotalMilliseconds}ms.");
        })
        { }

        public CodeTimer(Action<TimeSpan> report)
        {
            this.reportFunction = report;

            this.stopwatch.Start();
        }

        public void Dispose()
        {
            this.stopwatch.Stop();
            this.reportFunction(this.stopwatch.Elapsed);
        }
    }
}
