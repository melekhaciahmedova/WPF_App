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
            var directoryPath = "testDirectory";
            Directory.CreateDirectory(directoryPath);
            var filePath = Path.Combine(directoryPath, "testFile.txt");
            var service = new FileWatcherService(directoryPath, 1000);

            var eventTriggered = false;
            service.FileCreated += (path) =>
            {
                if (path == filePath)
                {
                    eventTriggered = true;
                }
            };

            service.Start();
            File.WriteAllText(filePath, "test content");
          await  Task.Delay(2000);

            Assert.True(eventTriggered);

            service.Stop();
            File.Delete(filePath);
            Directory.Delete(directoryPath);
        }
    }
}