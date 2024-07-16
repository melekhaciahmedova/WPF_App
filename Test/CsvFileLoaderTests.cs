using TradeDataMonitor.Models;
using TradeDataMonitorApp.Loaders;

namespace Test
{
    public class CsvFileLoaderTests
    {
        [Fact]
        public void LoadData_ShouldReturnCorrectTradeData()
        {
            // Arrange
            var csvContent = "Date,Open,High,Low,Close,Volume\n" +
                             "2013-5-20,30.16,30.39,30.02,30.17,1478200\n";
            var filePath = "test.csv";
            File.WriteAllText(filePath, csvContent);
            var loader = new CsvFileLoader();
            // Act
            IEnumerable<TradeData> result = loader.LoadData(filePath);

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