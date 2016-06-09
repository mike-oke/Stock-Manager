using System;
using System.Collections.Generic;
using BusinessLayer.Entities;

namespace BusinessLayer.Services.Stocks
{
    public class StockService
    {
        #region Dividend Yield

        /// <summary>
        /// Calculate Common Dividend
        /// </summary>
        /// <param name="lastDividend"></param>
        /// <returns></returns>
        private static double CalculateCommonDividend(double lastDividend)
        {
            return lastDividend;
        }

        /// <summary>
        /// Calculate Preferred Dividend
        /// </summary>
        /// <param name="fixedDividend"></param>
        /// <param name="parValue"></param>
        /// <returns>Return the result of the calculation</returns>
        private static double CalculatePreferredDividend(double fixedDividend, double parValue)
        {
            double result = 0.0;

            if (fixedDividend == 0.0 || parValue == 0.0)
                return result;

            result = fixedDividend * parValue;

            return result; 
        }

        /// <summary>
        /// Get dividend by stock type
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        private static double GetDividendByStockType(Stock stock)
        {
            double dividend = 0.0;

            if (stock == null)
                return dividend;

            if (stock.Type == (int)StockType.COMMON)
                dividend = CalculateCommonDividend(stock.LastDividend);
            else
                dividend = CalculatePreferredDividend(stock.FixedDividend, stock.PerValue);

            return dividend;
        }

        /// <summary>
        /// Calculate Dividend Yield.
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="marketPrice"></param>
        /// <returns></returns>
        private static double CalculateDividendYield(double dividend, double marketPrice)
        {
            double dividendYeld = 0.0;

            if (dividend == 0.0 || marketPrice == 0.0)
                return dividendYeld;

            dividendYeld = dividend / marketPrice;

            return dividendYeld;
        }

        /// <summary>
        /// Get dividend yield.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="marketPrice"></param>
        /// <returns></returns>
        public static double CalculateDividendYield(Stock stock, double marketPrice)
        {
            return CalculateDividendYield(GetDividendByStockType(stock), marketPrice);
        }

        #endregion

        #region P/E Ratio

        /// <summary>
        /// Calculate P/E Ratio
        /// </summary>
        /// <param name="marketPrice"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        private static double CalculatePERatio(double marketPrice, double dividend) 
        {
            return marketPrice / dividend;
        }

        /// <summary>
        /// Calculate P/E Ratio
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="marketPrice"></param>
        /// <returns></returns>
        public static double CalculatePERatio(Stock stock, double marketPrice) 
        {
            return CalculatePERatio(GetDividendByStockType(stock), marketPrice);
        }

        #endregion

        #region Geometric Mean

        /// <summary>
        /// Calculate Geometric Mean of prices
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
       private static double CalculateGeometricMean(double[] prices)
       {
            double productPrices = 1.0;

           foreach(double price in prices)
               productPrices = productPrices * price;
           
           double nPower = 1.0 / prices.Length;

           return Math.Pow(productPrices, nPower);
       }

        /// <summary>
        /// Calculate Geometric Mean of Trades
        /// </summary>
        /// <param name="trades"></param>
        /// <returns></returns>
        public static double CalculateGeometricMean(List<Trade> trades)
        {
            double[] prices = new double[trades.Count];
            int index = 0;

            foreach (Trade trade in trades)
            {
                prices[index] = trade.LastTradePrice;
                index++;
            }

            return CalculateGeometricMean(prices);
        }

        #endregion

        #region Volume Weighted Stock Price

        /// <summary>
        /// Calculate the Volume Weighted Stock Price
        /// </summary>
        /// <param name="trades"></param>
        /// <returns></returns>
        public static double CalculateVolumeWeightedStockPrice(List<Trade> trades)
        {
            if (trades.Count > 0)
            {
                int min = 15;

                double totalWeightedPrice = 0.0;
                double totalQuantity = 0.0;

                double vwsp = 0.0;

                foreach (var trade in trades)
                {
                    double quantity = (double)trade.Quantity;

                    if (trade.LastTradeTime <= min)
                    {
                        totalWeightedPrice += (trade.LastTradePrice * quantity);
                        totalQuantity += quantity;
                    }
                }

                if (totalQuantity > 0 & totalWeightedPrice > 0.0)
                {
                    vwsp = totalWeightedPrice / totalQuantity;
                    return vwsp;
                }
            }

            return double.NaN;
        }

        #endregion

    }
}
