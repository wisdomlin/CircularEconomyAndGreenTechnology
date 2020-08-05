using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EconomicMoats.Standard
{
    // Demonstrates how to provide delegates to exectution dataflow blocks.
    public class DataflowExecutionBlocks
    {
        // Computes the number of zero bytes that the provided file
        // contains.
        public int CountBytes(string path)
        {
            byte[] buffer = new byte[1024];
            int totalZeroBytesRead = 0;
            using (var fileStream = File.OpenRead(path))
            {
                int bytesRead = 0;
                do
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    totalZeroBytesRead += buffer.Count(b => b == 0);
                } while (bytesRead > 0);
            }

            return totalZeroBytesRead;
        }

        // Asynchronously computes the number of zero bytes that the provided file 
        // contains.
        public async Task<int> CountBytesAsync(string path)
        {
            byte[] buffer = new byte[1024];
            int totalZeroBytesRead = 0;
            using (var fileStream = new FileStream(
               path, FileMode.Open, FileAccess.Read, FileShare.Read, 0x1000, true))
            {
                int bytesRead = 0;
                do
                {
                    // Asynchronously read from the file stream.
                    bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    totalZeroBytesRead += buffer.Count(b => b == 0);
                } while (bytesRead > 0);
            }

            return totalZeroBytesRead;
        }
    }
}
