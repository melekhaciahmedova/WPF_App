using System.IO;
using System.Threading.Tasks;
using TradeDataMonitorApp.Services;
using Xunit;

namespace TradeDataMonitor.Tests
{
    public class FileWatcherServiceTests
    {
        [Fact]
        public async Task FileWatcher_ShouldTriggerFileCreatedEvent()
        {
            // Arrange
            var directoryPath = "testDirectory";
            Directory.CreateDirectory(directoryPath);
            var filePath = Path.Combine(directoryPath, "testFile.txt");
            var service = new FileWatcherService(directoryPath, 1000); // 1 saniye aralıkla kontrol

            var eventTriggered = false;
            service.FileCreated += (path) =>
            {
                if (path == filePath)
                {
                    eventTriggered = true;
                }
            };

            // Act
            service.Start();
            File.WriteAllText(filePath, "test content");
          await  Task.Delay(2000); // 2 saniye bekle

            // Assert
            Assert.True(eventTriggered);

            // Clean up
            service.Stop();
            File.Delete(filePath);
            Directory.Delete(directoryPath);
        }
    }
}