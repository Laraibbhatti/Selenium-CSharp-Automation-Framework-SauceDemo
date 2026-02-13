using AventStack.ExtentReports;
using courseproject.Core;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace courseproject.Checkout
{
    public class CheckoutPage : BasePage
    {
        #region Locators

        // Checkout Information
        public static By FirstNameTXT = By.Id("first-name");
        public static By LastNameTXT = By.Id("last-name");
        public static By PostalCodeTXT = By.Id("postal-code");
        public static By ContinueBTN = By.Id("continue");

        // Overview & Finish
        public static By FinishBTN = By.Id("finish");
        public static By ConfirmBTN = By.CssSelector("#checkout_complete_container > h2");
        public static By CancelBTN = By.Id("cancel");

        // Confirmation
        public static By CompleteHeaderLBL = By.ClassName("complete-header");
        public static By CompleteTextLBL = By.ClassName("complete-text");
        public static By BackHomeBTN = By.Id("back-to-products");

        // Error
        public static By ErrorMessage = By.CssSelector("#checkout_info_container > div > form > div.checkout_info > div.error-message-container.error");

        #endregion

        #region Verification Methods

        public bool IsCheckoutInfoPageDisplayed()
        {
            try
            {
                bool displayed = Driver.FindElement(FirstNameTXT).Displayed;
                TakeScreenShot(Status.Pass, "Checkout information page displayed");
                return displayed;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Checkout information page not displayed: {ex.Message}");
                throw;
            }
        }

        public bool IsFinishButtonDisplayed()
        {
            try
            {
                bool displayed = Driver.FindElement(FinishBTN).Displayed;
                TakeScreenShot(Status.Pass, "Finish button displayed");
                return displayed;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Finish button not displayed: {ex.Message}");
                throw;
            }
        }

        public string GetOrderSuccessHeader()
        {
            try
            {
                string text = Driver.FindElement(CompleteHeaderLBL).Text;
                TakeScreenShot(Status.Pass, "Order success header verified");
                return text;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to get order success header: {ex.Message}");
                throw;
            }
        }

        public string GetOrderSuccessMessage()
        {
            try
            {
                string text = Driver.FindElement(CompleteTextLBL).Text;
                TakeScreenShot(Status.Pass, "Order success message verified");
                return text;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to get order success message: {ex.Message}");
                throw;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                string error = Driver.FindElement(ErrorMessage).Text;
                TakeScreenShot(Status.Pass, "Checkout error message displayed");
                return error;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to get error message: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Action Methods

        public bool EnterCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            try
            {
                Driver.FindElement(FirstNameTXT).Clear();
                Driver.FindElement(FirstNameTXT).SendKeys(firstName);
                TakeScreenShot(Status.Pass, $"Entered First Name: {firstName}");

                Driver.FindElement(LastNameTXT).Clear();
                Driver.FindElement(LastNameTXT).SendKeys(lastName);
                TakeScreenShot(Status.Pass, $"Entered Last Name: {lastName}");

                Driver.FindElement(PostalCodeTXT).Clear();
                Driver.FindElement(PostalCodeTXT).SendKeys(postalCode);
                TakeScreenShot(Status.Pass, $"Entered Postal Code: {postalCode}");

                return true;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to enter checkout information: {ex.Message}");
                throw;
            }
        }

        public bool ClickContinue()
        {
            try
            {
                Driver.FindElement(ContinueBTN).Click();
                TakeScreenShot(Status.Pass, "Clicked Continue button");
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to click Continue button: {ex.Message}");
                throw;
            }
        }

        public bool FinishCheckout()
        {
            try
            {
                Driver.FindElement(FinishBTN).Click();
                TakeScreenShot(Status.Pass, "Clicked Finish button");
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to click Finish button: {ex.Message}");
                throw;
            }
        }

        public bool ConfirmCheckout()
        {
            try
            {
                Driver.FindElement(ConfirmBTN).Click();
                TakeScreenShot(Status.Pass, "Clicked Confirm button");
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to click Confirm button: {ex.Message}");
                throw;
            }
        }

        public void CancelCheckout()
        {
            try
            {
                Driver.FindElement(CancelBTN).Click();
                TakeScreenShot(Status.Pass, "Checkout cancelled");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to cancel checkout: {ex.Message}");
                throw;
            }
        }

        public void BackToHome()
        {
            try
            {
                Driver.FindElement(BackHomeBTN).Click();
                TakeScreenShot(Status.Pass, "Returned to products page");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                TakeScreenShot(Status.Fail, $"Failed to go back to home: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}
