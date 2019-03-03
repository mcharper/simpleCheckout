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
        public void GetPriceInvokesGetPriceListOnPriceListRepositoryDuringConstruction()
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

            priceListRepository.Verify(p => p.GetPriceList(), Times.Once);
        }

        [TestMethod]
        public void GetPriceInvokesGetOfferListOnOfferListRepositoryDuringConstruction()
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

            offerListRepository.Verify(o => o.GetOfferList(), Times.Once);
        }

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

        [TestMethod]
        public void GetPriceReturnsUnitPriceIfThereIsOneItemWhetherThereIsAnOfferOrNot()
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
                {
                    { 'A', new Tuple<int, int>(3, 130) }
                };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var price = sut.GetPrice('A', 1);

            Assert.AreEqual(priceList['A'], price);
        }

        [DataRow('A', 1, 50)]
        [DataRow('A', 2, 100)]
        [DataRow('A', 3, 150)]
        [DataRow('A', 4, 200)]
        [DataRow('A', 5, 250)]
        [DataTestMethod]
        public void GetPriceComputesPriceCorrectlyUsingUnitPriceIfThereAreNoOffers(char itemCode, int qty, int expectedTotal)
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
                {
                };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var total = sut.GetPrice(itemCode, qty);

            Assert.AreEqual(expectedTotal, total);
        }

        [DataRow('A', 1, 50)]
        [DataRow('A', 2, 100)]
        [DataTestMethod]
        public void GetPriceComputesPriceUsingUnitPriceIfQtyDoesNotQualifyForOffer(char itemCode, int qty, int expectedTotal)
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
            {
                { 'A', new Tuple<int, int>(3, 130) }
            };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var total = sut.GetPrice(itemCode, qty);

            Assert.AreEqual(expectedTotal, total);
        }

        [DataRow('A', 3, 130)]
        [DataTestMethod]
        public void GetPriceComputesPriceUsingOfferPriceIfQtyDoesJustQualifiesForOffer(char itemCode, int qty, int expectedTotal)
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
            {
                { 'A', new Tuple<int, int>(3, 130) }
            };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var total = sut.GetPrice(itemCode, qty);

            Assert.AreEqual(expectedTotal, total);
        }

        [DataRow('A', 6, 260)]
        [DataRow('A', 9, 390)]
        [DataTestMethod]
        public void GetPriceComputesPriceUsingOfferPriceIfQtyIsAMultipleOfOffer(char itemCode, int qty, int expectedTotal)
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
            {
                { 'A', new Tuple<int, int>(3, 130) }
            };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var total = sut.GetPrice(itemCode, qty);

            Assert.AreEqual(expectedTotal, total);
        }

        [DataRow('A', 4, 180)]
        [DataRow('A', 5, 230)]
        [DataTestMethod]
        public void GetPriceComputesPriceCorrectlyUsingOfferPriceAndUnitPriceIfQtyIsEnoughForOfferButNotExactMultiple(char itemCode, int qty, int expectedTotal)
        {
            var priceList = new Dictionary<char, int>()
                {
                    { 'A', 50 }
                };

            var offerList = new Dictionary<char, Tuple<int, int>>()
            {
                { 'A', new Tuple<int, int>(3, 130) }
            };

            var priceListRepository = new Mock<IPriceListRepository>();
            var offerListRepository = new Mock<IOfferListRepository>();

            priceListRepository.Setup(p => p.GetPriceList()).Returns(priceList);
            offerListRepository.Setup(o => o.GetOfferList()).Returns(offerList);

            var sut = new Pricer(priceListRepository.Object, offerListRepository.Object);
            var total = sut.GetPrice(itemCode, qty);

            Assert.AreEqual(expectedTotal, total);
        }
    }
}
