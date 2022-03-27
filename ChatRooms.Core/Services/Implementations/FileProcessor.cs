using FirstReact.Core.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChatRooms.Core.Services.Implementations
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger<IFileProcessor> _logger;

        public FileProcessor(ILogger<IFileProcessor> logger)
        {
            _logger = logger;
        }

        public Dictionary<string, List<string>> ProcessCsvFile(Stream stream)
        {
            Dictionary<string, List<string>> contents = new Dictionary<string, List<string>>();
            try
            {                
                using (StreamReader reader = new StreamReader(stream))
                {
                    var header = reader.ReadLine().Split(',');//header
                    for (int i = 0; i < header.Length; i++)
                    {
                        contents.Add((header[i]), new List<string>());
                    }
                    
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().Split(',');
                        for (int i = 0; i < header.Length; i++)
                        {
                            contents[header[i]].Add(line[i]);
                        }
                    }
                }                
            }
            catch(Exception e)
            {
                _logger.LogError($"An error has occurred when processing csv file: {e.Message}");
                throw;
            }
            return contents;
        }
    }
}
