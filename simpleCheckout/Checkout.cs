﻿using simpleCheckout.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace simpleCheckout
{
    public class Checkout : ICheckout
    {
        private IPricer _pricer;
        private const string _validItemCode = "^([A-Z])$";
        private List<char> _basket = new List<char>();

        public Checkout(IPricer pricer)
        {
            _pricer = pricer;
        }

        public void Scan(string item)
        {
            // TODO - Validate whether there is a known price for the item

            if(string.IsNullOrEmpty(item))
            {
                throw new ItemCodeMissingException();
            }

            if (!Regex.IsMatch(item, _validItemCode))
            {
                throw new ItemCodeInvalidException();
            }

            _basket.Add(item[0]);
        }

        public int GetTotalPrice()
        {
            var totalPrice = 0;

            var groupedBasket = _basket
                            .GroupBy(itemCode => itemCode)
                            .Select(itemCode => new
                            {
                                ItemCode = itemCode.Key,
                                Quantity = itemCode.Count()
                            }).ToList();

            foreach(var group in groupedBasket)
            {
                totalPrice += _pricer.GetPrice(group.ItemCode, group.Quantity);
            }

            return totalPrice;
        }
    }
}
