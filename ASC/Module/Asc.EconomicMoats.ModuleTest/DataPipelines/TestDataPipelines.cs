
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using System.Linq;
using System.Net.Http;


namespace Asc
{
    class TestDataPipelines
    {
        [Test]
        public void UC01_OneAsyncConsumers()
        {
            // Create a BufferBlock<byte[]> object. This object serves as the 
            // target block for the producer and the source block for the consumer.
            var buffer = new System.Threading.Tasks.Dataflow.BufferBlock<byte[]>();
            var Dpc = new DataflowProducerConsumer();

            // Start the consumer. The Consume method runs asynchronously. 
            var consumer = Dpc.ConsumeAsync(buffer);

            // Post source data to the dataflow block.
            Dpc.Produce(buffer);

            // Wait for the consumer to process all data.
            consumer.Wait();

            // Print the count of bytes processed to the console.
            Console.WriteLine("Processed {0} bytes.", consumer.Result);
        }

        [Test]
        public void UC02_TenAsyncConsumers()
        {
            // Create a BufferBlock<byte[]> object. This object serves as the 
            // target block for the producer and the source block for the consumer.
            var buffer = new BufferBlock<byte[]>();
            var Dpc = new DataflowProducerConsumer();

            // Start the consumer. The Consume method runs asynchronously. 
            List<Task> consumers = new List<Task>();
            for (int j = 1; j <= 10; j++)
            {
                consumers.Add(Dpc.ConsumeAsyncAndTryReceive(buffer));
            }


            // Post source data to [the BufferBlock].
            Dpc.Produce(buffer);

            // Wait for the consumer to process all data.
            foreach (Task consumer in consumers)
            {
                consumer.Wait();
            }

            // Print the count of bytes processed to the console.
            int i = 1;
            foreach (Task<int> consumer in consumers)
            {
                Console.WriteLine("Consumer " + i + " Processed {0} bytes.", consumer.Result);
                i++;
            }
        }

        [Test]
        public void UC03_DataflowReadWriteInt()
        {
            // Create a BufferBlock<int> object.
            var bufferBlock = new System.Threading.Tasks.Dataflow.BufferBlock<int>();
            var Drw = new DataflowReadWrite();

            // Post several messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            // Receive the messages back from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }

            // Post more messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            // TryReceive the messages back from the block.
            int value;
            while (bufferBlock.TryReceive(out value))
            {
                Console.WriteLine(value);
            }

            // Write to and read from the message block concurrently.
            var post01 = Task.Run(() =>
            {
                bufferBlock.Post(0);
                bufferBlock.Post(1);
            });
            var receive = Task.Run(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(bufferBlock.Receive());
                }
            });
            var post2 = Task.Run(() =>
            {
                bufferBlock.Post(2);
            });
            Task.WaitAll(post01, receive, post2);

            /* Sample output:
               2
               0
               1
             */

            // Demonstrate asynchronous dataflow operations.
            Drw.AsyncSendReceive(bufferBlock).Wait();
        }

        [Test]
        public void UC03_DataflowReadWriteString()
        {
            // Create a BufferBlock<int> object.
            var bufferBlock = new BufferBlock<string>();
            var Drw = new DataflowReadWrite();

            // Post several messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post("String,Start," + i + ",End");
            }

            // Receive the messages back from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }

            // Post more messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post("String,Start," + i + ",End");
            }

            // TryReceive the messages back from the block.
            string value;
            while (bufferBlock.TryReceive(out value))
            {
                Console.WriteLine(value);
            }

            // Write to and read from the message block concurrently.
            var post01 = Task.Run(() =>
            {
                bufferBlock.Post("String,Start,0,End");
                bufferBlock.Post("String,Start,1,End");
            });
            var receive = Task.Run(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(bufferBlock.Receive());
                }
            });
            var post2 = Task.Run(() =>
            {
                bufferBlock.Post("String,Start,2,End");
            });
            Task.WaitAll(post01, receive, post2);

            /* Sample output:
               2
               0
               1
             */

            // Demonstrate asynchronous dataflow operations.
            Drw.AsyncSendReceive(bufferBlock).Wait();
        }

        //[Test]
        //public void UC03_DataflowReadWriteStringArray()
        //{
        //    // Create a BufferBlock<int> object.
        //    var bufferBlock = new BufferBlock<string[]>();
        //    var Drw = new DataflowReadWrite();

        //    // Post several messages to the block.
        //    for (int i = 0; i < 3; i++)
        //    {
        //        string[] arr = { "Start-", i.ToString(), "-End" };
        //        bufferBlock.Post(arr);
        //    }

        //    // Receive the messages back from the block.
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Console.WriteLine(bufferBlock.Receive());
        //    }

        //    // Post more messages to the block.
        //    for (int i = 0; i < 3; i++)
        //    {
        //        string[] arr = { "Start-", i.ToString(), "-End" };
        //        bufferBlock.Post(arr);
        //    }

        //    // TryReceive the messages back from the block.
        //    string[] value;
        //    while (bufferBlock.TryReceive(out value))
        //    {
        //        Console.WriteLine(value);
        //    }

        //    // Write to and read from the message block concurrently.
        //    var post01 = Task.Run(() =>
        //    {
        //        string[] arr0 = { "Start-", (0).ToString(), "-End" };
        //        bufferBlock.Post(arr0);
        //        string[] arr1 = { "Start-", (1).ToString(), "-End" };
        //        bufferBlock.Post(arr1);
        //    });
        //    var receive = Task.Run(() =>
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            Console.WriteLine(bufferBlock.Receive());
        //        }
        //    });
        //    var post2 = Task.Run(() =>
        //    {
        //        string[] arr2 = { "Start-", (2).ToString(), "-End" };
        //        bufferBlock.Post(arr2);
        //    });
        //    Task.WaitAll(post01, receive, post2);

        //    /* Sample output:
        //       2
        //       0
        //       1
        //     */

        //    // Demonstrate asynchronous dataflow operations.
        //    Drw.AsyncSendReceive(bufferBlock).Wait();
        //}

        [Test]
        public void UC04_DataflowExecutionBlocks_CountBytesSync()
        {
            // Create a temporary file on disk.
            string tempFile = Path.GetTempFileName();

            // Write random data to the temporary file.
            using (var fileStream = File.OpenWrite(tempFile))
            {
                Random rand = new Random();
                byte[] buffer = new byte[1024];
                for (int i = 0; i < 512; i++)
                {
                    rand.NextBytes(buffer);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }

            // Create an ActionBlock<int> object that prints to the console 
            // the number of bytes read.
            var printResult = new ActionBlock<int>(zeroBytesRead =>
            {
                Console.WriteLine("{0} contains {1} zero bytes.",
                   Path.GetFileName(tempFile), zeroBytesRead);
            });

            // Create a TransformBlock<string, int> object that calls the 
            // CountBytes function and returns its result.
            DataflowExecutionBlocks Deb = new DataflowExecutionBlocks();
            var countBytes = new TransformBlock<string, int>(
               new Func<string, int>(Deb.CountBytes));

            // Link the TransformBlock<string, int> object to the 
            // ActionBlock<int> object.
            countBytes.LinkTo(printResult);

            // Create a continuation task that completes the ActionBlock<int>
            // object when the TransformBlock<string, int> finishes.
            countBytes.Completion.ContinueWith(delegate { printResult.Complete(); });

            // Post the path to the temporary file to the 
            // TransformBlock<string, int> object.
            countBytes.Post(tempFile);

            // Requests completion of the TransformBlock<string, int> object.
            countBytes.Complete();

            // Wait for the ActionBlock<int> object to print the message.
            printResult.Completion.Wait();

            // Delete the temporary file.
            File.Delete(tempFile);
        }

        [Test]
        public void UC05_DataflowExecutionBlocks_CountBytesAsync()
        {
            // Create a temporary file on disk.
            string tempFile = Path.GetTempFileName();

            // Write random data to the temporary file.
            using (var fileStream = File.OpenWrite(tempFile))
            {
                Random rand = new Random();
                byte[] buffer = new byte[1024];
                for (int i = 0; i < 512; i++)
                {
                    rand.NextBytes(buffer);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }

            // -----------------------------------------------------------------
            // Create an ActionBlock<int> object that prints to the console 
            // the number of bytes read.
            var printResult = new ActionBlock<int>(zeroBytesRead =>
            {
                Console.WriteLine("{0} contains {1} zero bytes.",
                   Path.GetFileName(tempFile), zeroBytesRead);
            });

            // Create a TransformBlock<string, int> object that calls the 
            // CountBytes function and returns its result.
            // Asynchronous by Func<TResult>
            // When a dataflow block behaves asynchronously, 
            //   the task of the dataflow block is complete only when the returned Task<TResult> object finishes.
            var countBytes = new TransformBlock<string, int>(
               new Func<string, Task<int>>(
                   new DataflowExecutionBlocks().CountBytesAsync));

            // Link the TransformBlock<string, int> object to the 
            // ActionBlock<int> object.
            countBytes.LinkTo(printResult);

            // -----------------------------------------------------------------
            // Get a CompletionTask that represents the Completion of TransformBlock<string, int>.
            // For the CompletionTask, Set a ContinuationAction that completes the ActionBlock<int>.
            countBytes.Completion.ContinueWith(delegate { printResult.Complete(); });

            // Post [the path of temporary file] to [TransformBlock<string, int> object].
            // i.e., feed the string Input for TransformBlock<string, int>
            countBytes.Post(tempFile);

            // Requests completion of the TransformBlock<string, int> object.
            countBytes.Complete();

            // Wait for the ActionBlock<int> object to print the message.
            printResult.Completion.Wait();

            // Delete the temporary file.
            File.Delete(tempFile);
        }

        [Test]
        public void UC06_BuildDataflowPipeline_DataflowReversedWords()
        {
            // Demonstrates how to create a basic dataflow pipeline.
            // This program downloads the book "The Iliad of Homer" by Homer from the Web 
            // and finds all reversed words that appear in that book.

            // --------------------------------------
            // Create the members of the pipeline.
            // --------------------------------------

            // Downloads the requested resource as a string.
            var downloadString = new TransformBlock<string, string>(async uri =>
            {
                Console.WriteLine("Downloading '{0}'...", uri);

                return await new HttpClient().GetStringAsync(uri);
            });

            // Separates the specified text into an array of words.
            var createWordList = new TransformBlock<string, string[]>(text =>
            {
                Console.WriteLine("Creating word list...");

                // Remove common punctuation by replacing all non-letter characters 
                // with a space character.
                char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
                text = new string(tokens);

                // Separate the text into an array of words.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            });

            // Removes short words and duplicates.
            var filterWordList = new TransformBlock<string[], string[]>(words =>
            {
                Console.WriteLine("Filtering word list...");

                return words
                   .Where(word => word.Length > 3)
                   .Distinct()
                   .ToArray();
            });

            // Finds all words in the specified collection whose reverse also exists in the collection.
            var findReversedWords = new TransformManyBlock<string[], string>(words =>
            {
                Console.WriteLine("Finding reversed words...");

                var wordsSet = new HashSet<string>(words);

                return from word in words.AsParallel()
                       let reverse = new string(word.Reverse().ToArray())
                       where word != reverse && wordsSet.Contains(reverse)
                       select word;
            });

            // Prints the provided reversed words to the console.    
            var printReversedWords = new ActionBlock<string>(reversedWord =>
            {
                Console.WriteLine("Found reversed words {0}/{1}",
                   reversedWord, new string(reversedWord.Reverse().ToArray()));
            });

            // --------------------------------------
            // Connect the dataflow blocks to form a pipeline.
            // --------------------------------------

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            downloadString.LinkTo(createWordList, linkOptions);
            createWordList.LinkTo(filterWordList, linkOptions);
            filterWordList.LinkTo(findReversedWords, linkOptions);
            findReversedWords.LinkTo(printReversedWords, linkOptions);

            // Process "The Iliad of Homer" by Homer.
            downloadString.Post("http://www.gutenberg.org/cache/epub/16452/pg16452.txt");

            // Mark the head of the pipeline as complete.
            //     The head of the pipeline propagates its completion after it processes all buffered messages.
            // If you send more than one input through a pipeline, 
            //     call the IDataflowBlock.Complete method after you submit all the input.
            // If your application has no well-defined point at which data is no longer available, 
            //     you can omit this step.
            downloadString.Complete();

            // Wait for the last DataflowBlock in the pipeline to process all messages.
            printReversedWords.Completion.Wait();

        }

        [Test]
        public void UC07_DataflowReceiveAny()
        {
            // Create a shared CancellationTokenSource object to enable the 
            // TrySolution method to be cancelled.
            var cts = new System.Threading.CancellationTokenSource();

            DataflowReceiveAny dataflowReceiveAny = new DataflowReceiveAny();

            // Create three TransformBlock<int, int> objects. 
            // Each TransformBlock<int, int> object calls the TrySolution method.
            Func<int, int> transform = n => dataflowReceiveAny.TrySolution(n, cts.Token);
            var trySolution1 = new TransformBlock<int, int>(transform);
            var trySolution2 = new TransformBlock<int, int>(transform);
            var trySolution3 = new TransformBlock<int, int>(transform);

            // Post data to each TransformBlock<int, int> object.
            trySolution1.Post(11);
            trySolution2.Post(21);
            trySolution3.Post(31);

            // Call the ReceiveFromAny<T> method to receive the result from the 
            // first TransformBlock<int, int> object to finish.
            int result = dataflowReceiveAny.ReceiveFromAny(trySolution1, trySolution2, trySolution3);

            // Cancel all calls to TrySolution that are still active.
            cts.Cancel();

            // Print the result to the console.
            Console.WriteLine("The solution is {0}.", result);

            cts.Dispose();
        }

        [Test]
        public async Task UC08_TestHttpClient()
        {
            var url = "http://webcode.me";
            using var client = new HttpClient();

            var HttpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
            Console.WriteLine(HttpResponse);

            var HttpResponseBody = await client.GetStringAsync(url);
            Console.WriteLine(HttpResponseBody);
        }
    }
}
