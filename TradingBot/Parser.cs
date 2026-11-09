using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using System.Globalization;

namespace TradingBot
{
    public class CsvCandleReader
    {
        public List<Candle> Read(string path)
        {
            var candles = new List<Candle>();

            using (var sr = new StreamReader(path))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] values = line.Split(',');

                    long raw = long.Parse(values[0], CultureInfo.InvariantCulture);
                    DateTimeOffset time = values[0].Length >= 16
                        ? DateTimeOffset.FromUnixTimeMilliseconds(raw / 1000)
                        : DateTimeOffset.FromUnixTimeMilliseconds(raw);

                    decimal open = decimal.Parse(values[1], CultureInfo.InvariantCulture);
                    decimal high = decimal.Parse(values[2], CultureInfo.InvariantCulture);
                    decimal low = decimal.Parse(values[3], CultureInfo.InvariantCulture);
                    decimal close = decimal.Parse(values[4], CultureInfo.InvariantCulture);
                    decimal volume = decimal.Parse(values[5], CultureInfo.InvariantCulture);

                    candles.Add(new Candle(time, open, high, low, close, volume));
                }
            }

            return candles;
        }
    }
}
