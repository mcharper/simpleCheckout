using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var sut = new Checkout();

            try
            {
                sut.Scan(itemCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("Checkout.Scan should accept valid input");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeMissingException))]
        public void CheckoutScanThrowsExceptionForBlankInput()
        {
            var sut = new Checkout();

            sut.Scan("");
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void CheckoutScanThrowsExceptionForNumericInput()
        {
            var sut = new Checkout();

            sut.Scan("1");
        }

        [TestMethod]
        [ExpectedException(typeof(ItemCodeInvalidException))]
        public void CheckoutScanThrowsExceptionForTooLongInput()
        {
            var sut = new Checkout();

            sut.Scan("AA");
        }

        [TestMethod]
        public void CheckoutGetTotalPriceReturnsZeroIfBasketIsEmpty()
        {
            var sut = new Checkout();

            var totalPrice = sut.GetTotalPrice();

            Assert.AreEqual(0, totalPrice);
        }
    }
}
