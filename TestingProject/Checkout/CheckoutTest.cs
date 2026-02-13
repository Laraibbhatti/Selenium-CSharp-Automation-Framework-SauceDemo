using courseproject.Core;
using courseproject.Login;
using courseproject.Products;
using courseproject.Cart;
using courseproject.Checkout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace courseproject.CheckoutTests
{
    [TestClass]
    public class CheckoutTest : ExtentReport
    {
        LoginPage loginPage = new LoginPage();
        ProductsPage productsPage = new ProductsPage();
        CartPage cartPage = new CartPage();
        CheckoutPage checkoutPage = new CheckoutPage();
        BasePage basePage = new BasePage();

        public TestContext TestContext { get; set; }

        #region Initialization and Cleanup

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("CheckoutTest class initialization");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("CheckoutTest class cleanup");
        }

        [TestInitialize]
        public void TestInit()
        {
            Console.WriteLine("CheckoutTest initialization");
            basePage.SeleniumInit();
            exParentTest = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("CheckoutTest cleanup");
            basePage.DriverClose();
            extentReports.Flush();
        }

        #endregion

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml", "CheckoutTestCase_001_SingleProduct", DataAccessMethod.Sequential)]
        public void CheckoutTestCase_001_SingleProduct()
        {
            exChildTest = exParentTest.CreateNode("Checkout - Single Product");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string firstName = TestContext.DataRow["firstName"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string postalCode = TestContext.DataRow["postalCode"].ToString();
            string expectedHeader = TestContext.DataRow["successHeader"].ToString();

            loginPage.Login(url, username, password);

            productsPage.AddBackpackToCart();
            productsPage.OpenCart();
            cartPage.ProceedToCheckout(); 
            checkoutPage.EnterCheckoutInformation(firstName, lastName, postalCode);
            checkoutPage.ClickContinue();
            checkoutPage.FinishCheckout();
            checkoutPage.ConfirmCheckout();

            string actualHeader = checkoutPage.GetOrderSuccessHeader();
            Assert.AreEqual(expectedHeader, actualHeader);

            exChildTest.Pass("Single product checkout successful");
        }

      
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml", "CheckoutTestCase_002_MultipleProducts", DataAccessMethod.Sequential)]
        public void CheckoutTestCase_002_MultipleProducts()
        {
            exChildTest = exParentTest.CreateNode("Checkout - Multiple Products");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string firstName = TestContext.DataRow["firstName"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string postalCode = TestContext.DataRow["postalCode"].ToString();
            string expectedHeader = TestContext.DataRow["successHeader"].ToString();

            loginPage.Login(url, username, password);

            productsPage.AddMultipleProducts();
            productsPage.OpenCart();
            cartPage.ProceedToCheckout();
            checkoutPage.EnterCheckoutInformation(firstName, lastName, postalCode);
            checkoutPage.ClickContinue();
            checkoutPage.FinishCheckout();

            string actualHeader = checkoutPage.GetOrderSuccessHeader();
            Assert.AreEqual(expectedHeader, actualHeader);


            exChildTest.Pass("Multiple products checkout successful");
        }


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
"Datacourse.xml", "CheckoutTestCase_003_EmptyFirstName", DataAccessMethod.Sequential)]
        public void CheckoutTestCase_003_EmptyFirstName()
        {
            exChildTest = exParentTest.CreateNode("Checkout - Empty First Name");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string postalCode = TestContext.DataRow["postalCode"].ToString();
            string expectedError = TestContext.DataRow["errorMessage"].ToString();

            loginPage.Login(url, username, password);

            productsPage.AddBackpackToCart();
            productsPage.OpenCart();
            cartPage.ProceedToCheckout();
            checkoutPage.EnterCheckoutInformation("", lastName, postalCode);
            checkoutPage.ClickContinue();

            string actualError = checkoutPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Pass("Validation message displayed for empty first name");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
"Datacourse.xml", "CheckoutTestCase_004_EmptyPostalCode", DataAccessMethod.Sequential)]
        public void CheckoutTestCase_004_EmptyPostalCode()
        {
            exChildTest = exParentTest.CreateNode("Checkout - Empty Postal Code");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string firstName = TestContext.DataRow["firstName"].ToString();
            string lastName = TestContext.DataRow["lastName"].ToString();
            string expectedError = TestContext.DataRow["errorMessage"].ToString();

            loginPage.Login(url, username, password);

            productsPage.AddBackpackToCart();
            productsPage.OpenCart();
            cartPage.ProceedToCheckout();

            checkoutPage.EnterCheckoutInformation(firstName, lastName, "");
            checkoutPage.ClickContinue();

            string actualError = checkoutPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Pass("Validation message displayed for empty postal code");
        }

    }
}
