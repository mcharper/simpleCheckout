
using simpleCheckout.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace simpleCheckout
{
    public class Pricer : IPricer
    {
        private const string _validItemCode = "^([A-Z])$";

        public int GetPrice(char itemCode, int quantity)
        {
            if (!Regex.IsMatch(itemCode.ToString(), _validItemCode))
            {
                throw new ItemCodeInvalidException();
            }

            if (quantity < 1)
            {
                throw new QuantityInvalidException();
            }

            return 0;
        }
    }
}
