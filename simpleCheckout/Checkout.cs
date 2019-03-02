using simpleCheckout.Exceptions;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace simpleCheckout
{
    public class Checkout : ICheckout
    {
        private const string validItemCode = "^([A-Z])$";

        private string basket = string.Empty;
        private StringBuilder basketBuilder = new StringBuilder();

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

            basketBuilder.Append(item);
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
