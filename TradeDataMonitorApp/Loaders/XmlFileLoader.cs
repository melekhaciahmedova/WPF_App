using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using TradeDataMonitorApp.Models;

namespace TradeDataMonitorApp.Loaders
{
    public class XmlFileLoader : IFileLoader
    {
        public IEnumerable<TradeData> LoadData(string filePath)
        {
            var document = XDocument.Load(filePath);
            var tradeDataList = new List<TradeData>();

            foreach (var element in document.Descendants("value"))
            {
                var tradeData = new TradeData
                {
                    Date = element.Attribute("date").Value,
                    Open = decimal.Parse(element.Attribute("open").Value, CultureInfo.InvariantCulture),
                    High = decimal.Parse(element.Attribute("high").Value, CultureInfo.InvariantCulture),
                    Low = decimal.Parse(element.Attribute("low").Value, CultureInfo.InvariantCulture),
                    Close = decimal.Parse(element.Attribute("close").Value, CultureInfo.InvariantCulture),
                    Volume = int.Parse(element.Attribute("volume").Value)
                };

                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }
    }
}