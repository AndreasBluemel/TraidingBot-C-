using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    public class Indicators
    {
        public static List<decimal?> Sma(IReadOnlyList<Candle> candles, int period)
        {

            if (period < 1)
                throw new ArgumentException("Period must be at least 1.", nameof(period));

            var sma = new List<decimal?>();

            for (int i = 0; i < candles.Count; i++)
            {
                if (i < period - 1)
                {
                    sma.Add(null);
                }
                else
                {
                    decimal sum = 0;
                    for (int j = i - period + 1; j <= i; j++)
                    {
                        sum += candles[j].Close;
                    }
                    sma.Add(sum / period);
                }
            }

            return sma;
        }
    }
      
}
