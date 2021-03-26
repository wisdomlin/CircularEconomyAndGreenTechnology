using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;


namespace Asc
{
    // Demonstrates how to unlink dataflow blocks.
    public class DataflowReceiveAny
    {
        // Receives the value from the first provided source that has 
        // a message.
        public T ReceiveFromAny<T>(params ISourceBlock<T>[] sources)
        {
            // Create a WriteOnceBlock<T> object and link it to each source block.
            var writeOnceBlock = new WriteOnceBlock<T>(e => e);
            foreach (var source in sources)
            {
                // [Setting MaxMessages to one] instructs
                // [the source block] to unlink from [the WriteOnceBlock<T> object]
                // after [offering the WriteOnceBlock<T> object one message].
                source.LinkTo(writeOnceBlock, new DataflowLinkOptions { MaxMessages = 1 });
            }
            // Return the first value that is offered to the WriteOnceBlock object.
            return writeOnceBlock.Receive();
        }

        // Demonstrates a function that takes several seconds to produce a result.
        public int TrySolution(int n, CancellationToken ct)
        {
            // Simulate a lengthy operation that completes within three seconds
            // or when the provided CancellationToken object is cancelled.
            SpinWait.SpinUntil(() =>
                ct.IsCancellationRequested, new Random().Next(3000));

            // Return a value.
            return n + 42;
        }
    }
}
