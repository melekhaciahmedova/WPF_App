using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using TradeDataMonitorApp.Loaders;
using TradeDataMonitorApp.Models;
using TradeDataMonitorApp.Services;

namespace TradeDataMonitorApp
{
    public partial class MainWindow : Window
    {
        private readonly FileWatcherService _fileWatcherService;
        private readonly ObservableCollection<TradeData> _tradeDataCollection;

        public MainWindow()
        {
            InitializeComponent();

            _tradeDataCollection = new ObservableCollection<TradeData>();
            TradeDataGrid.ItemsSource = _tradeDataCollection;

            var directoryPath = ConfigurationManager.AppSettings["InputDirectory"];
            var interval = int.Parse(ConfigurationManager.AppSettings["CheckInterval"]);
            _fileWatcherService = new FileWatcherService(directoryPath, 5000);
            _fileWatcherService.FileCreated += OnFileCreated;
            _fileWatcherService.Start();
        }

        private void OnFileCreated(string filePath)
        {
            IFileLoader loader = GetFileLoader(filePath);

            if (loader != null)
            {
                var data = loader.LoadData(filePath);
                foreach (var item in data)
                {
                    Application.Current.Dispatcher.Invoke(() => _tradeDataCollection.Add(item));
                }
            }
        }

        private IFileLoader GetFileLoader(string filePath)
        {
            var extension = System.IO.Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".csv":
                    return new CsvFileLoader();
                case ".txt":
                    return new TxtFileLoader();
                case ".xml":
                    return new XmlFileLoader();
                default:
                    return null;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _fileWatcherService.Stop();
        }
    }
}