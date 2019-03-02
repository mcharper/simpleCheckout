using simpleCheckout.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

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
            var totalPrice = 0;

            var groupedBasket = basket
                            .GroupBy(itemCode => itemCode)
                            .Select(itemCode => new
                            {
                                ItemCode = itemCode.Key,
                                Quantity = itemCode.Count(),
                                Price = 0
                            }).ToList();

            foreach(var x in groupedBasket)
            {
                var priceForGroup = 0;
                totalPrice += priceForGroup;
            }

            return totalPrice;
        }
    }
}
