using System;

namespace BusinessLayer.Entities
{
    public class Stock
    {
        public string Symbol { get; set; }
        public int Type { get; set; }
        public double LastDividend { get; set; }
        public double FixedDividend { get; set; }
        public double PerValue { get; set; }

    }
}
