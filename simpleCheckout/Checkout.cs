using System;
using System.Text.RegularExpressions;

namespace simpleCheckout
{
    public class Checkout : ICheckout
    {
        private const string validItemCode = "^([A-Z])$";

        public void Scan(string item)
        {
            if(string.IsNullOrEmpty(item))
            {
                throw new Exception();
            }

            if (!Regex.IsMatch(item, validItemCode))
            {
                throw new Exception();
            }
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
