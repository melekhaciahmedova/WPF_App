using System.Collections.Generic;
using TradeDataMonitorApp.Models;

namespace TradeDataMonitorApp.Loaders
{
    public interface IFileLoader
    {
        IEnumerable<TradeData> LoadData(string filePath);
    }
}