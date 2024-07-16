using System;
using System.IO;
using System.Timers;

namespace TradeDataMonitorApp.Services
{
    public class FileWatcherService
    {
        private readonly Timer _timer;
        private readonly string _directoryPath;

        public event Action<string> FileCreated;

        public FileWatcherService(string directoryPath, double interval)
        {
            _directoryPath = directoryPath;
            _timer = new Timer(interval);
            _timer.Elapsed += CheckForNewFiles;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void CheckForNewFiles(object sender, ElapsedEventArgs e)
        {
            var files = Directory.GetFiles(_directoryPath);
            foreach (var file in files)
            {
                FileCreated?.Invoke(file);
            }
        }
    }
}