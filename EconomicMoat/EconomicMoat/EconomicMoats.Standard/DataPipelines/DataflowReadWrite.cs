using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EconomicMoat.Standard
{
    // Demonstrates a how to write to and read from a dataflow block.
    public class DataflowReadWrite
    {
        // Demonstrates asynchronous dataflow operations.
        public async Task AsyncSendReceive(BufferBlock<int> bufferBlock)
        {
            // Asynchronously post the messages to the block.
            for (int i = 0; i < 3; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            // Asynchronously receive the messages back from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(await bufferBlock.ReceiveAsync());
            }

            /* Output:
               0
               1
               2
             */
        }

        public async Task AsyncSendReceive(BufferBlock<string> bufferBlock)
        {
            // Asynchronously post the messages to the block.
            for (int i = 0; i < 3; i++)
            {
                await bufferBlock.SendAsync("String,Start," + i + ",End");
            }

            // Asynchronously receive the messages back from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(await bufferBlock.ReceiveAsync());
            }

            /* Output:
               0
               1
               2
             */
        }
    }
}
