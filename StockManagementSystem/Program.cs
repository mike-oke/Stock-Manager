using System;
using System.Collections.Generic;
using BusinessLayer.Entities;
using BusinessLayer.Services.Stocks;

namespace StockManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variable declaration

            int userSelectedOption = 0;
            double marketPrice = 0.0;
            double result = 0.0;

            #endregion

            #region Utility

            //Display stock manager menu
            while (userSelectedOption != 5)
            {
                userSelectedOption = DisplayStockManagerMenu();

                if (userSelectedOption == 5)
                    return;


                if (userSelectedOption == 1 || userSelectedOption == 2) 
                {
                    //Get market price value
                    Console.WriteLine("Please input the market price and hit the enter key:");
                    marketPrice = getMarketPriceValue();
                }

                //Take action
                if (userSelectedOption == 1) //Calculate dividend yield
                {
                    Stock stock = new Stock()
                    {
                        Symbol = "ALE",
                        Type = (int)StockType.COMMON,
                        LastDividend = 23.0,
                        FixedDividend = 0.0,
                        PerValue = 60.0
                    };

                    result = StockService.CalculateDividendYield(stock, marketPrice);
                }
                else if (userSelectedOption == 2) //calculate P/E Ratio
                {
                    Stock stock = new Stock()
                    {
                        Symbol = "GIN",
                        Type = (int)StockType.PREFERRED,
                        LastDividend = 8.0,
                        FixedDividend = 0.2,
                        PerValue = 100.0
                    };

                    result = StockService.CalculatePERatio(stock, marketPrice);
                }
                else if (userSelectedOption == 3) //Calculate Volume Weighted Stock Price 
                {
                    List<Trade> trades = new List<Trade>();

                    trades.Add(new Trade() { ID = "TEA", LastTradeTime = 6, Quantity = 10, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.10 });
                    trades.Add(new Trade() { ID = "POP", LastTradeTime = 3, Quantity = 18, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.85 });
                    trades.Add(new Trade() { ID = "ALE", LastTradeTime = 26, Quantity = 26, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.18 });
                    trades.Add(new Trade() { ID = "GIN", LastTradeTime = 10, Quantity = 33, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.77 });
                    trades.Add(new Trade() { ID = "JOE", LastTradeTime = 8, Quantity = 60, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.29 });

                    result = StockService.CalculateVolumeWeightedStockPrice(trades);
                }
                else if (userSelectedOption == 4) //Calculate GBCE for all share Indexes 
                {
                    try
                    {
                        List<Trade> trades = new List<Trade>();

                        trades.Add(new Trade() { ID = "TEA", LastTradeTime = 10, Quantity = 10, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.10 });
                        trades.Add(new Trade() { ID = "POP", LastTradeTime = 8, Quantity = 18, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.85 });
                        trades.Add(new Trade() { ID = "ALE", LastTradeTime = 17, Quantity = 26, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.18 });
                        trades.Add(new Trade() { ID = "GIN", LastTradeTime = 14, Quantity = 33, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.77 });
                        trades.Add(new Trade() { ID = "JOE", LastTradeTime = 12, Quantity = 60, Indicator = (int)IndicatorType.BUY, LastTradePrice = 0.29 });

                        result = StockService.CalculateGeometricMean(trades);
                    }
                    catch
                    {
                        result = 0;
                    }
                }
                else
                {
                    //take no action
                }

                //Print the result
                Console.WriteLine("The result of your action is:" + result);
            }

            Console.ReadKey();

            #endregion
        }

        #region Methods

        /// <summary>
        /// Display Stock Manager Menu
        /// </summary>
        /// <returns></returns>
        private static int DisplayStockManagerMenu()
        {
            Console.WriteLine("Enter the number corresponding to the action that you would like to perform and press <ENTER> ");
            Console.WriteLine("1. Calculate dividend yield. " + Environment.NewLine + "2. Calculate P/E Ratio" + Environment.NewLine +
                              "3. Calculate Volume Weighted Stock Price" + Environment.NewLine + "4. Calculate GBCE for all shares" + Environment.NewLine + "5. End" + Environment.NewLine);

            //Get the option selected
            int selectedOptionValue;
            try
            {
                selectedOptionValue = int.Parse(Console.ReadLine());
            }
            catch
            {
                selectedOptionValue = 0;
            }

            return selectedOptionValue;
        }

        /// <summary>
        /// Get market price based on user input
        /// </summary>
        /// <returns></returns>
        private static double getMarketPriceValue()
        {
            try
            {
                //Get the market price and parse the result
                double marketPrice = double.Parse(Console.ReadLine());
                return marketPrice;
            }
            catch
            {
                //if there is am error, catch it and return 0
                Console.WriteLine("Error in input!");
                return 0.0;
            }
        }

        #endregion
    }
}
