using AventStack.ExtentReports;
using courseproject.Core;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace courseproject.Cart
{
    public class CartPage : BasePage
    {
        #region Locators

        public static By RemoveBTN = By.XPath("//button[text()='Remove']");
        public static By CheckoutBTN = By.Id("checkout");

        public static By CartItem = By.ClassName("cart_item");
        public static By CartTitleLBL = By.ClassName("title");
        public static By ErrorMessage = By.CssSelector("h3[data-test='error']");

        #endregion

        #region Methods

        public bool RemoveProductFromCart()
        {
            try
            {
                var removeButtons = Driver.FindElements(RemoveBTN);

                if (removeButtons.Count == 0)
                {
                    TakeScreenShot(Status.Fail, "No Remove button found");
                    return false;
                }

                removeButtons[0].Click();
                TakeScreenShot(Status.Pass, "Clicked Remove button");
                Thread.Sleep(1000);

                bool isItemPresent = Driver.FindElements(CartItem).Count > 0;

                if (!isItemPresent)
                {
                    TakeScreenShot(Status.Pass, "Product removed successfully");
                    return true;
                }

                TakeScreenShot(Status.Fail, "Product still exists in cart");
                return false;
            }
            catch (Exception)
            {
                TakeScreenShot(Status.Fail, "Failed to remove product from cart");
                throw;
            }
        }

        public bool ProceedToCheckout()
        {
            try
            {
                Driver.FindElement(CheckoutBTN).Click();
                TakeScreenShot(Status.Pass, "Clicked Checkout button");
                Thread.Sleep(1000);

                string title = Driver.FindElement(CartTitleLBL).Text;
                return title.Contains("Checkout");
            }
            catch (Exception)
            {
                TakeScreenShot(Status.Fail, "Failed to click Checkout button");
                throw;
            }
        }

        public string GetCartTitle()
        {
            Thread.Sleep(1000);
            return Driver.FindElement(CartTitleLBL).Text;
        }

        public string GetCheckoutBTN()
        {
            Thread.Sleep(1000);
            return Driver.FindElement(CheckoutBTN).Text;
        }

        public string GetErrorMessage()
        {
            Thread.Sleep(1000);
            return Driver.FindElement(ErrorMessage).Text;
        }

        #endregion
    }
}
