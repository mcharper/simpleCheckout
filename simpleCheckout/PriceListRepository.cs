using System;
using System.Collections.Generic;
using System.Text;

namespace simpleCheckout
{
    public class PriceListRepository : IPriceListRepository
    {
        private Dictionary<char, int> _priceList = new Dictionary<char, int>()
        {
            { 'A', 50 },
            { 'B', 30 },
            { 'C', 20 },
            { 'D', 15 }
        };

        public Dictionary<char, int> GetPriceList()
        {
            return _priceList;
        }
    }
}
