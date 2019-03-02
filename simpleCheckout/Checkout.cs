using simpleCheckout.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace simpleCheckout
{
    public class Checkout : ICheckout
    {
        private const string validItemCode = "^([A-Z])$";

        private List<char> basket = new List<char>();

        public void Scan(string item)
        {
            if(string.IsNullOrEmpty(item))
            {
                throw new ItemCodeMissingException();
            }

            if (!Regex.IsMatch(item, validItemCode))
            {
                throw new ItemCodeInvalidException();
            }

            basket.Add(item[0]);
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
