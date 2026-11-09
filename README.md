# Trading Bot (C# / .NET 8)

A learning project: building an algorithmic trading bot from scratch in C#.
The goal is to understand market data, strategy testing, risk management
and clean software architecture, **not** to make money.

> ⚠️ Disclaimer: This project is for educational purposes only.
> It is not financial advice. Do not use it with real money.

## Status

🚧 Work in progress (Week 1 of ~12)

## Roadmap

- [x] Project setup
- [ ] Load historical OHLCV data from CSV
- [ ] Indicators (SMA)
- [ ] Strategy interface + SMA crossover strategy
- [ ] Backtester (fees, slippage, in-sample / out-of-sample split)
- [ ] Risk manager (position sizing, daily loss limit)
- [ ] Paper trading via exchange testnet
- [ ] Logging and error handling

## Tech Stack

- C# / .NET 8
- xUnit for unit tests
- (planned) Binance.Net for the testnet connection

## Project Structure

```text
csharp-trading-bot/
├── src/
│   └── TradingBot/            # Main application
│       ├── Models/            # Candle and other domain types
│       ├── Data/              # CSV reader, candle sources
│       ├── Indicators/        # SMA and other indicators (planned)
│       ├── Strategies/        # IStrategy + implementations (planned)
│       ├── Backtesting/       # Backtester, metrics (planned)
│       └── Program.cs
├── tests/
│   └── TradingBot.Test/       # xUnit unit tests
├── data/                      # Historical CSV files (not tracked by Git)
├── docs/                      # Glossary, dev log
├── TradingBot.slnx
└── README.md
```



## Data Format

Historical data comes from [data.binance.vision](https://data.binance.vision)
(`spot → monthly → klines → BTCUSDT → 1d`). Place the CSV files in `data/`.

Each row is one candle, comma-separated, **without a header**:

| # | Field | Example |
|---|-------|---------|
| 1 | Open time (Unix, microseconds or milliseconds) | 1790812800000000 |
| 2 | Open | 83623.59 |
| 3 | High | 85273.65 |
| 4 | Low | 83186.00 |
| 5 | Close | 84880.05 |
| 6 | Volume (base asset, BTC) | 16766.06 |
| 7 | Close time | 1790899199999999 |
| 8 | Quote asset volume (USDT) | 1409485416.76 |
| 9 | Number of trades | 3018436 |
| 10-12 | Taker buy volumes, ignored | |

Only fields 1-6 are used. Prices are parsed with `CultureInfo.InvariantCulture`
and stored as `decimal`.

## Development Log

Short notes on what I learned and what went wrong: see `docs/devlog.md`.
