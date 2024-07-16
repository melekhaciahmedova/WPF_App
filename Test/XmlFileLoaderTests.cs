using System.Collections.Generic;
using System.IO;
using TradeDataMonitorApp.Loaders;
using TradeDataMonitorApp.Models;
using Xunit;

namespace TradeDataMonitor.Tests
{
    public class XmlFileLoaderTests
    {
        [Fact]
        public void LoadData_ShouldReturnCorrectTradeData()
        {
            // Arrange
            var xmlContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" +
                             "<values>\n" +
                             " <value date=\"2013-5-20\" open=\"30.16\" high=\"30.39\" low=\"30.02\" close=\"30.17\" volume=\"1478200\" />\n" +
                             "</values>";
            var filePath = "test.xml";
            File.WriteAllText(filePath, xmlContent);
            var loader = new XmlFileLoader();

            // Act
            var result = loader.LoadData(filePath);

            // Assert
            var tradeDataList = new List<TradeData>(result);
            Assert.Single(tradeDataList);
            var tradeData = tradeDataList[0];
            Assert.Equal("2013-5-20", tradeData.Date);
            Assert.Equal(30.16m, tradeData.Open);
            Assert.Equal(30.39m, tradeData.High);
            Assert.Equal(30.02m, tradeData.Low);
            Assert.Equal(30.17m, tradeData.Close);
            Assert.Equal(1478200, tradeData.Volume);

            // Clean up
            File.Delete(filePath);
        }
    }
}