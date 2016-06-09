using System;

namespace BusinessLayer.Entities
{
    public class Trade
    {
        public string ID { get; set; }

        public int LastTradeTime { get; set; }

        public int Quantity { get; set; }

        public int Indicator { get; set; }

        public double LastTradePrice { get; set; }

    }
}
