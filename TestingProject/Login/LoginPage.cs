using AventStack.ExtentReports;
using courseproject.Core;
using OpenQA.Selenium;
using System.Threading;

namespace courseproject.Login
{
    public class LoginPage : BasePage
    {
        #region Locators
        public static By usernameTXT = By.Id("user-name");
        public static By passwordTXT = By.Id("password");
        public static By LoginBTN = By.Id("login-button");
        public static By ProductLBL = By.ClassName("title");
        public static By ErrorMessage = By.CssSelector(
            "#login_button_container > div > form > div.error-message-container.error > h3");
        #endregion
       
        #region Methods

        public void Login(string url, string username, string password)
        { 
            Driver.Url = url;
            TakeScreenShot(Status.Pass, "Enter URL");
            Thread.Sleep(1000); 

            Driver.FindElement(usernameTXT).SendKeys(username);
            TakeScreenShot(Status.Pass, "Enter Username");
            Thread.Sleep(1000); 

            Driver.FindElement(passwordTXT).SendKeys(password);
            TakeScreenShot(Status.Pass, "Enter Password");
            Thread.Sleep(1000);

            Driver.FindElement(LoginBTN).Click();
            TakeScreenShot(Status.Pass, "Click on Login Button");
            Thread.Sleep(1000); 
        }

        public string GetProductTitle()
        {
            Thread.Sleep(1000); 
            return Driver.FindElement(ProductLBL).Text;
        }

        public string GetErrorMessage()
        {
            Thread.Sleep(1000); 
            return Driver.FindElement(ErrorMessage).Text;
        }

        #endregion
    }
}
