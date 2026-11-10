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

        IStrategy strategy = new SmaCrossoverStrategy(10, 30);

        for (int i = 0; i < allCandles.Count; i++)
        {
            Signal s = strategy.Decide(allCandles, i);

            if (s != Signal.Hold)
                Console.WriteLine($"{allCandles[i].Time:yyyy-MM-dd} {s,-4} Close: {allCandles[i].Close:0.00}");
        }

        BacktestResult result = new Backtester().Run(allCandles, strategy);
        Console.WriteLine($"Strategy:     {result.ReturnPercent:0.00}%  ({result.TradeCount} trades)");
        Console.WriteLine($"Buy and hold: {result.BuyAndHoldPercent:0.00}%");
        Console.WriteLine($"Final equity: {result.FinalEquity:0.00}");
    }
}

