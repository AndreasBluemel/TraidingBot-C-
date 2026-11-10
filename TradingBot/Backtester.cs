using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TradingBot
{
    public class BacktestResult
    {
        public decimal FinalEquity { get; init; }
        public decimal ReturnPercent { get; init; }
        public decimal BuyAndHoldPercent { get; init; }
        public int TradeCount { get; init; }
    }

    public class Backtester
    {
        public BacktestResult Run(IReadOnlyList<Candle> candles, IStrategy strategy, decimal initialCash = 10_000m, decimal feeRate = 0.001m)
        {
            decimal money = initialCash;
            decimal btc = 0;
            int trade = 0;

            for (int i = 0; i < candles.Count - 1; i++)
            {
                decimal price = candles[i + 1].Open;
                Signal s = strategy.Decide(candles, i);
                if(s == Signal.Buy && btc == 0 )
                {
                    btc = money / price * (1 - feeRate);
                    money = 0;

                    trade++;
                }
                else if(s == Signal.Sell && btc != 0)
                {
                    money = btc * price * (1 - feeRate);
                    btc = 0;
                    trade++;
                }
            }

            decimal finalEquity = money + btc * candles[candles.Count - 1].Close;
            int start = 30;   // TODO: лучше передавать из стратегии, пока достаточно константы
            decimal bhBtc = initialCash / candles[start].Open * (1 - feeRate);
            decimal bhEquity = bhBtc * candles[candles.Count - 1].Close;

            return new BacktestResult
            {
                FinalEquity = finalEquity,
                ReturnPercent = (finalEquity / initialCash - 1m) * 100m,
                BuyAndHoldPercent = (bhEquity / initialCash - 1m) * 100m,
                TradeCount = trade
            };
        }
    }
}
