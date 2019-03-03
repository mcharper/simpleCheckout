using System;
using System.Collections.Generic;
using System.Text;

namespace simpleCheckout
{
    public interface IPriceListRepository
    {
        Dictionary<char, int> GetPriceList();
    }
}
