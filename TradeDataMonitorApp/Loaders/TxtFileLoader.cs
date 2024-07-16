using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TradeDataMonitorApp.Models;

namespace TradeDataMonitorApp.Loaders
{
    public class TxtFileLoader : IFileLoader
    {
        public IEnumerable<TradeData> LoadData(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var tradeDataList = new List<TradeData>();

            foreach (var line in lines)
            {
                if (line.StartsWith("Date")) continue;

                var columns = line.Split(';');
                if (columns.Length == 6)
                {
                    var tradeData = new TradeData
                    {
                        Date = columns[0],
                        Open = decimal.Parse(columns[1], CultureInfo.InvariantCulture),
                        High = decimal.Parse(columns[2], CultureInfo.InvariantCulture),
                        Low = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                        Close = decimal.Parse(columns[4], CultureInfo.InvariantCulture),
                        Volume = int.Parse(columns[5])
                    };

                    tradeDataList.Add(tradeData);
                }
            }

            return tradeDataList;
        }
    }
}