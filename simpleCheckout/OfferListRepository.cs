using System;
using System.Collections.Generic;
using System.Text;

namespace simpleCheckout
{
    public class OfferListRepository : IOfferListRepository
    {
        private Dictionary<char, Tuple<int, int>> _offerList = new Dictionary<char, Tuple<int, int>>()
        {
            { 'A', new Tuple<int, int>(3, 130) },
            { 'B', new Tuple<int, int>(2, 45) },
        };

        public Dictionary<char, Tuple<int, int>> GetOfferList()
        {
            return (_offerList);
        }
    }
}

