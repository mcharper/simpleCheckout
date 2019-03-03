
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

        private IPriceListRepository _priceListRepository;
        private IOfferListRepository _offerListRepository;

        private Dictionary<char, int> _priceList;
        private Dictionary<char, Tuple<int,int>> _offerList;

        public Pricer(IPriceListRepository priceListRepository, IOfferListRepository offerListRepository)
        {
            _priceListRepository = priceListRepository;
            _offerListRepository = offerListRepository;

            _priceList = _priceListRepository.GetPriceList();
            _offerList = _offerListRepository.GetOfferList();
        }

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

            int unitPrice = _priceList[itemCode];

            if (_offerList.ContainsKey(itemCode))
            {
                int offerBatchSize = _offerList[itemCode].Item1; // eg: 3 x item A
                int offerBatchPrice = _offerList[itemCode].Item2; // for 130

                int numberOfBatches = quantity / offerBatchSize;
                int remainingUnits = quantity % offerBatchSize;

                price = numberOfBatches * offerBatchPrice + remainingUnits * unitPrice;
            }
            else
            {
                price = quantity * unitPrice;
            }

            return price;
        }
    }
}
