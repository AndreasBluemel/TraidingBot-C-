using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    public enum Signal { Buy, Sell, Hold }

    public interface IStrategy
    {
        Signal Decide(IReadOnlyList<Candle> candles, int index);
    }

    public class SmaCrossoverStrategy : IStrategy
    {
        private readonly int _fastPeriod;
        private readonly int _slowPeriod;

        public SmaCrossoverStrategy(int fastPeriod, int slowPeriod)
        {
            _fastPeriod = fastPeriod;
            _slowPeriod = slowPeriod;
        }

        public Signal Decide(IReadOnlyList<Candle> candles, int index)
        {
            if (index < _slowPeriod)
                return Signal.Hold;

            var visible = candles.Take(index + 1).ToList();

            var fast = Indicators.Sma(visible, _fastPeriod);
            var slow = Indicators.Sma(visible, _slowPeriod);


            if (fast[index] == null || slow[index] == null || fast[index - 1] == null || slow[index - 1] == null)
                return Signal.Hold;

            if (fast[index - 1] <= slow[index - 1] && fast[index] > slow[index])
            {
                return Signal.Buy;
            }
            else if (fast[index - 1] >= slow[index - 1] && fast[index] < slow[index])
            {
                return Signal.Sell;
            }

            return Signal.Hold;
        }
    }

}
