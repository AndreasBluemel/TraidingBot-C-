using System.Globalization;
using System.IO;
using TradingBot;
//using TradingBot.Data;

class Program
{
    static void Main(string[] args)
    {
        List<Candle> allCandles = new List<Candle>();
        CsvCandleReader reader = new CsvCandleReader();
        string folder = @"C:\Users\aefro\source\repos\AndreasBluemel\TraidingBot-C-\data";

        foreach (string file in Directory.GetFiles(folder, "*.csv"))
        {
            var candles = reader.Read(file);
            allCandles.AddRange(candles);
            Console.WriteLine($"{Path.GetFileName(file)}: {candles.Count} candles");
        }

        for (int i = 1; i < allCandles.Count; i++)
        {
            if (allCandles[i].Time - allCandles[i - 1].Time != TimeSpan.FromDays(1))
                Console.WriteLine($"There are gap before {allCandles[i].Time:yyyy-MM-dd}");
        }

        allCandles = allCandles.OrderBy(c => c.Time).ToList();

        Console.WriteLine($"Total: {allCandles.Count} candles");
        Console.WriteLine($"From {allCandles.First().Time:yyyy-MM-dd} to {allCandles.Last().Time:yyyy-MM-dd}");

        //var sma10 = Indicators.Sma(allCandles, 10);

        //for (int i = 0; i < 15; i++)
        //{
        //    Console.WriteLine($"{allCandles[i].Time:yyyy-MM-dd} Close: {allCandles[i].Close} SMA10: {sma10[i]}");
        //}

        var fast = Indicators.Sma(allCandles, 10);
        var slow = Indicators.Sma(allCandles, 30);

        for (int i = 1; i < allCandles.Count; i++)
        {
            if (fast[i] == null || slow[i] == null || fast[i - 1] == null || slow[i - 1] == null)
                continue;

            if (fast[i - 1] <= slow[i - 1] && fast[i] > slow[i])
            {
                Console.WriteLine($"{allCandles[i].Time:yyyy-MM-dd} BUY Close: {allCandles[i].Close:0.00}");
            }
            else if (fast[i - 1] >= slow[i - 1] && fast[i] < slow[i])
            {
                Console.WriteLine($"{allCandles[i].Time:yyyy-MM-dd} SELL Close: {allCandles[i].Close:0.00}");
            }

        }
    }
}

