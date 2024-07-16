using System.Collections.Generic;
using System.IO;
using TradeDataMonitorApp.Loaders;
using TradeDataMonitorApp.Models;
using Xunit;

namespace TradeDataMonitor.Tests
{
    public class TxtFileLoaderTests
    {
        [Fact]
        public void LoadData_ShouldReturnCorrectTradeData()
        {
            var txtContent = "Date;Open;High;Low;Close;Volume\n" +
                             "2013-5-20;30.16;30.39;30.02;30.17;1478200\n";
            var filePath = "test.txt";
            File.WriteAllText(filePath, txtContent);
            var loader = new TxtFileLoader();

            var result = loader.LoadData(filePath);

            var tradeDataList = new List<TradeData>(result);
            Assert.Single(tradeDataList);
            var tradeData = tradeDataList[0];
            Assert.Equal("2013-5-20", tradeData.Date);
            Assert.Equal(30.16m, tradeData.Open);
            Assert.Equal(30.39m, tradeData.High);
            Assert.Equal(30.02m, tradeData.Low);
            Assert.Equal(30.17m, tradeData.Close);
            Assert.Equal(1478200, tradeData.Volume);

            File.Delete(filePath);
        }
    }
}