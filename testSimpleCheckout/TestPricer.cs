using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using simpleCheckout;
using simpleCheckout.Exceptions;
using System;
using System.Collections.Generic;

namespace testSimpleCheckout
{
    [TestClass]
    public class TestPricer
    {
        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void GetPriceThrowsExceptionIfItemCodeIsInvalid()
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 },
                    { 'B', 30 },
                    { 'C', 20 },
                    { 'D', 15 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
                {
                    { 'A', new Tuple<int, int>(3, 130) },
                    { 'B', new Tuple<int, int>(2, 45) },
                };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);

            sut.GetPrice(' ', 1);
        }

        [TestMethod]
        [ExpectedException(typeof(QuantityInvalidException))]
        public void GetPriceThrowsExceptionIfQuantityIsInvalid()
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 },
                    { 'B', 30 },
                    { 'C', 20 },
                    { 'D', 15 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
                {
                    { 'A', new Tuple<int, int>(3, 130) },
                    { 'B', new Tuple<int, int>(2, 45) },
                };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);

            sut.GetPrice('A', -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeHasNoPriceException))]
        public void GetPriceThrowsExceptionIfItemCodeHasNoPrice()
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 },
                    { 'B', 30 },
                    { 'C', 20 },
                    { 'D', 15 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
                {
                    { 'A', new Tuple<int, int>(3, 130) },
                    { 'B', new Tuple<int, int>(2, 45) },
                };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);

            sut.GetPrice('Z', 1);
        }
    }
}
