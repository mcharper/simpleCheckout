
using simpleCheckout.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace simpleCheckout
{
    public class Pricer : IPricer
    {
        // TODO - Remove repetition of this
        private const string _validItemCode = "^([A-Z])$"; 

        // TODO - Extract from here
        private Dictionary<char, int> _priceList = new Dictionary<char, int>()
        {
            { 'A', 50 },
            { 'B', 30 },
            { 'C', 20 },
            { 'D', 15 }
        };

        // TODO - Extract from here
        private Dictionary<char, Tuple<int,int>> _offerList = new Dictionary<char, Tuple<int, int>>()
        {
            { 'A', new Tuple<int, int>(3, 130) },
            { 'B', new Tuple<int, int>(2, 45) },
        };

        public int GetPrice(char itemCode, int quantity)
        {
            int price = 0;

            if (!Regex.IsMatch(itemCode.ToString(), _validItemCode))
            {
                throw new ItemCodeInvalidException();
            }

            if (quantity < 1)
            {
                throw new QuantityInvalidException();
            }

            if (!_priceList.ContainsKey(itemCode))
            {
                throw new ItemCodeHasNoPriceException();
            }

            if (_offerList.ContainsKey(itemCode))
            {
                price = _offerList[itemCode].Item2;
            }
            else
            {
                price = _priceList[itemCode] * quantity;
            }

            return price;
        }
    }
}
