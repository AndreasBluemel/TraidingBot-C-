using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    public class Candle
    {
        public DateTimeOffset Time { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }

        public Candle(DateTimeOffset time, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
        public override string ToString()
        {
            return string.Create(System.Globalization.CultureInfo.InvariantCulture,
                $"{Time:yyyy-MM-dd} O:{Open:0.00} H:{High:0.00} L:{Low:0.00} C:{Close:0.00} V:{Volume:0.00}");
        }

    }
}
