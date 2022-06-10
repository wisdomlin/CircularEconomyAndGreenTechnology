using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NUnit.Framework;

namespace XXXEntity.UnitTest
{
    class TestProducerConsumer
    {
        private BufferBlock<string> buffer = new BufferBlock<string>();

        [Test]
        async Task TC01_ProcessFiles(IEnumerable<string> fileNames)
        {
            // start producing, but do not await:
            Task taskProduce = ProduceLinesAsync(fileNames);

            // because we did not await, we are free to do the following as soon as the
            // TextReader has to await for a line.
            // again, do not await.
            Task taskConsume = ConsumeAsync();

            // await until both the producer and the consumer are finished:
            await Task.WhenAll(new Task[] { taskProduce, taskConsume });
        }

        async Task ProduceLinesAsync(string fileName)
        {
            using (TextReader fileReader = File.OpenText(fileName))
            {
                string readLine = await fileReader.ReadLineAsync();
                while (readLine != null)
                {
                    // a line has been read; put it on the buffer:
                    await buffer.SendAsync(readLine);

                    // read the next line                
                    readLine = await fileReader.ReadLineAsync();
                }
            }
        }

        async Task ProduceLinesAsync(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                await ProduceLinesAsync(fileName);
            }

            // If here, nothing to produce anymore.
            // tell the buffer that producing is finished:
            buffer.Complete();
        }

        async Task ConsumeAsync()
        {
            while (await buffer.OutputAvailableAsync())
            {
                // there is something on the buffer; fetch it and process it:
                var line = await buffer.ReceiveAsync();
                this.ProcessLine(line);
            }

            // if here, producer marked Complete(), indicating that no data is to be expected
        }

        private void ProcessLine(string line)
        {
            //throw new System.NotImplementedException();
        }
    }
}
