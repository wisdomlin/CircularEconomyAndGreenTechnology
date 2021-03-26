using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Asc
{
    // Demonstrates a basic producer and consumer pattern that uses dataflow.
    public class DataflowProducerConsumer
    {
        // Demonstrates the production end of the producer and consumer pattern.
        public void Produce(ITargetBlock<byte[]> target)
        {
            // Create a Random object to generate random data.
            Random rand = new Random();

            // In a loop, fill a buffer with random data and
            // post the buffer to the target block.
            for (int i = 0; i < 100; i++)
            {
                // Create an array to hold random byte data.
                byte[] buffer = new byte[1024];

                // Fill the buffer with random bytes.
                rand.NextBytes(buffer);

                // Post the result to the message block.
                target.Post(buffer);
            }

            // Set the target to the completed state to signal to the consumer
            // that no more data will be available.
            target.Complete();
        }

        // Demonstrates the consumption end of the producer and consumer pattern.
        public async Task<int> ConsumeAsync(ISourceBlock<byte[]> source)
        {
            // Initialize a counter to track the number of bytes that are processed.
            int bytesProcessed = 0;

            // Read from the source buffer until the source buffer has no 
            // available output data.
            while (await source.OutputAvailableAsync())
            {
                byte[] data = source.Receive();

                // Increment the count of bytes received.
                bytesProcessed += data.Length;
            }

            return bytesProcessed;
        }

        // Demonstrates the consumption end of the producer and consumer pattern.
        public async Task<int> ConsumeAsyncAndTryReceive(IReceivableSourceBlock<byte[]> source)
        {
            // Initialize a counter to track the number of bytes that are processed.
            int bytesProcessed = 0;

            // Read from the source buffer until the source buffer has no available output data.
            while (await source.OutputAvailableAsync())
            {
                byte[] data;
                while (source.TryReceive(out data))
                {
                    // Increment the count of bytes received.
                    bytesProcessed += data.Length;
                    Thread.Sleep(100);  // Simulate some operations that need 100 ms
                }
            }

            return bytesProcessed;
        }
    }
}
