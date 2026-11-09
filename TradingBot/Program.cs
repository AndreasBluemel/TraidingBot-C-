using System.Globalization;
using System.IO;
using TradingBot;
//using TradingBot.Data;

class Program
{
    static void Main(string[] args)
    {
        var reader = new CsvCandleReader();
        var candles = reader.Read(@"C:\Users\aefro\source\repos\AndreasBluemel\TraidingBot-C-\data\BTCUSDT-1d-2026-10.csv");
        foreach (var c in candles.Take(10))
            Console.WriteLine(c);
    }
}

