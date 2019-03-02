using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using simpleCheckout.Exceptions;
using System;

namespace simpleCheckout
{
    [TestClass]
    public class TestCheckout
    {
        [DataRow("A")]
        [DataRow("B")]
        [DataRow("C")]
        [DataRow("D")]
        [DataRow("E")]
        [DataRow("F")]
        [DataRow("G")]
        [DataRow("H")]
        [DataRow("I")]
        [DataRow("J")]
        [DataRow("K")]
        [DataRow("L")]
        [DataRow("M")]
        [DataRow("N")]
        [DataRow("O")]
        [DataRow("P")]
        [DataRow("Q")]
        [DataRow("R")]
        [DataRow("S")]
        [DataRow("T")]
        [DataRow("U")]
        [DataRow("V")]
        [DataRow("W")]
        [DataRow("X")]
        [DataRow("Y")]
        [DataRow("Z")]
        [DataTestMethod]
        public void CheckoutScanAcceptsAlphaInput(string itemCode)
        {
            // Arrange
            var pricer = new Mock<IPricer>();
            pricer.Setup(a => a.GetPrice(It.IsAny<string>(), It.IsAny<int>())).Returns(0);
            var sut = new Checkout(pricer.Object);

            try
            {
                // Act
                sut.Scan(itemCode);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Fail("Checkout.Scan should accept valid input");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeMissingException))]
        public void CheckoutScanThrowsExceptionForBlankInput()
        {
            var pricer = new Mock<IPricer>();
            pricer.Setup(a => a.GetPrice(It.IsAny<string>(), It.IsAny<int>())).Returns(0);
            var sut = new Checkout(pricer.Object);

            sut.Scan("");
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void CheckoutScanThrowsExceptionForNumericInput()
        {
            var pricer = new Mock<IPricer>();
            pricer.Setup(a => a.GetPrice(It.IsAny<string>(), It.IsAny<int>())).Returns(0);
            var sut = new Checkout(pricer.Object);

            sut.Scan("1");
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void CheckoutScanThrowsExceptionForTooLongInput()
        {
            var pricer = new Mock<IPricer>();
            pricer.Setup(a => a.GetPrice(It.IsAny<string>(), It.IsAny<int>())).Returns(0);
            var sut = new Checkout(pricer.Object);

            sut.Scan("AA");
        }

        [TestMethod]
        public void CheckoutGetTotalPriceReturnsZeroIfBasketIsEmpty()
        {
            var pricer = new Mock<IPricer>();
            pricer.Setup(a => a.GetPrice(It.IsAny<string>(), It.IsAny<int>())).Returns(0);
            var sut = new Checkout(pricer.Object);

            var totalPrice = sut.GetTotalPrice();

            Assert.AreEqual(0, totalPrice);
        }
    }
}
