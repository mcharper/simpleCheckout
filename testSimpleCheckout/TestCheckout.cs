using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace simpleCheckout
{
    [TestClass]
    public class TestCheckout
    {
        [TestMethod]
        public void CheckoutScanAcceptsValidInput()
        {
            var sut = new Checkout();

            try
            {
                sut.Scan("A");
            }
            catch(Exception ex)
            {
                Assert.Fail("Checkout.Scan should accept valid input");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void CheckoutScanThrowsExceptionForBlankItemCode()
        {
            var sut = new Checkout();

            sut.Scan("");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void CheckoutScanThrowsExceptionForTooLongNonKeywordItemCode()
        {
            var sut = new Checkout();

            sut.Scan("AAA");
        }
    }
}
