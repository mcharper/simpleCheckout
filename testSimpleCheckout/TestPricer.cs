using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using simpleCheckout;
using simpleCheckout.Exceptions;
using System;

namespace testSimpleCheckout
{
    [TestClass]
    public class TestPricer
    {
        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void GetPriceThrowsExceptionIfItemCodeIsInvalid()
        {
            var sut = new Pricer();

            sut.GetPrice(' ', 1);
        }

        [TestMethod]
        [ExpectedException(typeof(QuantityInvalidException))]
        public void GetPriceThrowsExceptionIfQuantityIsInvalid()
        {
            var sut = new Pricer();

            sut.GetPrice('A', -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeHasNoPriceException))]
        public void GetPriceThrowsExceptionIfItemCodeHasNoPrice()
        {
            var sut = new Pricer();

            sut.GetPrice('Z', 1);
        }
    }
}
