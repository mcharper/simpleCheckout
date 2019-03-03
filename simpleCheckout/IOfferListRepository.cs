
using System;
using System.Collections.Generic;

namespace simpleCheckout
{
    public interface IOfferListRepository
    {
        Dictionary<char, Tuple<int, int>> GetOfferList();
    }
}
