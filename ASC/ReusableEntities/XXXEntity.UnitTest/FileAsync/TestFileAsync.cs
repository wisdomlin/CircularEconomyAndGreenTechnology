using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XXXEntity
{
    class TestFileAsync
    {
        [Test]
        public void XXX()
        {
            //var UC = new UseCase01();
            //UC.Run().Wait();
            this.Run().Wait();

            Console.WriteLine("Finished.");

        }

        /// <summary>
        /// UseCase.Run()
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            var testFilePath = "Test.txt";
            var lineCount = 5000000;

            await PrepareFile(testFilePath, lineCount);

            var warmUpCount = 1;
            var testCount = 5;

            using (var codetimer = new CodeTimer("All ReadLines"))
            {
                RunTests("ReadLines", () =>
                {
                    var lines = File.ReadAllLines(testFilePath);
                }, warmUpCount, testCount);
            }

            using (var codetimer = new CodeTimer("All Async ReadLines"))
            {
                await RunTestsAsync("Async ReadLines", async () =>
                {
                    var lines = await FileAsync.ReadAllLinesAsync(testFilePath);
                }, warmUpCount, testCount);
            }
        }

        /// <summary>
        /// Write a testfile of lineCount lines
        /// </summary>
        /// <param name="testFilePath"></param>
        /// <param name="lineCount"></param>
        /// <returns></returns>
        public async Task PrepareFile(string testFilePath, int lineCount)
        {
            if (!File.Exists(testFilePath))
            {
                using (var stream = new FileStream(testFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.Write, 4096, FileOptions.Asynchronous))
                using (var streamWriter = new StreamWriter(stream))
                {
                    for (var lineIndex = 0; lineIndex < lineCount; lineIndex++)
                    {
                        await streamWriter.WriteLineAsync(lineIndex.ToString());
                    }
                }
            }
        }

        public async Task RunTestsAsync(string testName, Func<Task> test, int warmUpCount, int testCount)
        {
            Console.WriteLine($"{testName} warming up.");

            for (var warmUpIndex = 0; warmUpIndex < warmUpCount; warmUpIndex++)
            {
                await test();
            }

            Console.WriteLine($"{testName} warmed up.");

            for (var testIndex = 0; testIndex < testCount; testIndex++)
            {
                using (var codetimer = new CodeTimer(testName))
                {
                    await test();
                }
            }
        }

        public void RunTests(string testName, Action test, int warmUpCount, int testCount)
        {
            Console.WriteLine($"{testName} warming up.");

            for (var warmUpIndex = 0; warmUpIndex < warmUpCount; warmUpIndex++)
            {
                test();
            }

            Console.WriteLine($"{testName} warmed up.");

            for (var testIndex = 0; testIndex < testCount; testIndex++)
            {
                using (var codetimer = new CodeTimer(testName))
                {
                    test();
                }
            }
        }
    }
}
