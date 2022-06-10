using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace XXXEntity
{
    public static class FileAsync
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultFileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static Task<string[]> ReadAllLinesAsync(string path)
        {
            return ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            var lines = new List<string>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultFileOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}
