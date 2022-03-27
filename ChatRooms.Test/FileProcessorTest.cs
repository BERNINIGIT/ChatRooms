using ChatRooms.Core.Services.Implementations;
using FirstReact.Core.Services.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using Xunit;

namespace ChatRooms.Test
{
    public class FileProcessorTest
    {
        [Fact]
        public void ProcessCsvFileTest()
        {
            //Arrange
            string fileName = "aapl.us.csv";
            string _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.GetFullPath(Path.Combine(_BaseDirectory, @"..\..\..\"));
            string finalPath = $"{fullPath}Resources\\{fileName}";
            Stream fs = File.OpenRead(finalPath);
            var loggerMock = new Mock<ILogger<IFileProcessor>>();
            var fileProcessor = new FileProcessor(loggerMock.Object);
            //Act
            var results = fileProcessor.ProcessCsvFile(fs);
            //Assert
            Assert.Equal(8, results.Count);
        }
    }
}
