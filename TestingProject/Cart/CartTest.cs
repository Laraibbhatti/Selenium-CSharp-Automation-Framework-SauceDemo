using courseproject.Core;
using courseproject.Login;
using courseproject.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace courseproject.Cart
{
    [TestClass]
    public class CartTest : ExtentReport
    {
        LoginPage loginPage = new LoginPage();
        ProductsPage productsPage = new ProductsPage();
        CartPage cartPage = new CartPage();
        BasePage basePage = new BasePage();

        public TestContext TestContext { get; set; }

        #region Initialization / Cleanup

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("CartTest class initialization");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("CartTest class cleanup");
        }

        [TestInitialize]
        public void TestInit()
        {
            Console.WriteLine("CartTest initialization");
            basePage.SeleniumInit();
            exParentTest = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("CartTest cleanup");
            basePage.DriverClose();
            extentReports.Flush();
        }

        #endregion

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_001_AddSingleProduct",
            DataAccessMethod.Sequential)]
        public void CartTestCase_001_AddSingleProduct()
        {
            exChildTest = exParentTest.CreateNode("Add Single Product");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product1"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Pass("Single product added to cart successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_002_AddMultipleProducts",
            DataAccessMethod.Sequential)]
        public void CartTestCase_002_AddMultipleProducts()
        {
            exChildTest = exParentTest.CreateNode("Add Multiple Products");

            loginPage.Login(
               TestContext.DataRow["url"].ToString(),
               TestContext.DataRow["username"].ToString(),
               TestContext.DataRow["password"].ToString());

            productsPage.AddMultipleProducts();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product1"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Pass("Multiple products added to cart");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_003_RemoveProduct",
            DataAccessMethod.Sequential)]
        public void CartTestCase_003_RemoveProduct()
        {
            exChildTest = exParentTest.CreateNode("Remove Product");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();
            productsPage.OpenCart();

            bool actualResult = cartPage.RemoveProductFromCart();
            Assert.IsTrue(actualResult);

            exChildTest.Pass("Product removed from cart");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_004_OpenEmptyCart",
            DataAccessMethod.Sequential)]
        public void CartTestCase_004_OpenEmptyCart()
        {
            exChildTest = exParentTest.CreateNode("Open Empty Cart");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Info("Opened empty cart successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_005_AddRemoveAddAgain",
            DataAccessMethod.Sequential)]
        public void CartTestCase_005_AddRemoveAddAgain()
        {
            exChildTest = exParentTest.CreateNode("Add Remove Add Again");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product1"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            cartPage.RemoveProductFromCart();

            BasePage.Driver.Navigate().Back();

            productsPage.EnsureBackpackAdded();

            string expectedCount1 = TestContext.DataRow["product"].ToString();
            string actualCount1 = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle1 = TestContext.DataRow["product1"].ToString();
            string actualTitle1 = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Pass("Add → Remove → Add again flow successful");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_006_CheckoutButtonVisible",
            DataAccessMethod.Sequential)]
        public void CartTestCase_006_CheckoutButtonVisible()
        {
            exChildTest = exParentTest.CreateNode("Checkout Button Visibility");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedButton = TestContext.DataRow["product1"].ToString();
            string actualButton = cartPage.GetCheckoutBTN();
            Assert.AreEqual(expectedButton, actualButton);

            exChildTest.Pass("Checkout button is visible");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_007_AddProductAfterBack",
            DataAccessMethod.Sequential)]
        public void CartTestCase_007_AddProductAfterBack()
        {
            exChildTest = exParentTest.CreateNode("Add Product After Back");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product1"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            BasePage.Driver.Navigate().Back();

            productsPage.AddBikeLightToCart();

            string expectedCount1 = TestContext.DataRow["product2"].ToString();
            string actualCount1 = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle1 = TestContext.DataRow["product1"].ToString();
            string actualTitle1 = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Pass("Product added after navigating back");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "Datacourse.xml",
            "CartTestCase_008_MultipleCartOpen",
            DataAccessMethod.Sequential)]
        public void CartTestCase_008_MultipleCartOpen()
        {
            exChildTest = exParentTest.CreateNode("Open Cart Multiple Times");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expectedCount = TestContext.DataRow["product"].ToString();
            string actualCount = productsPage.GetCartCount();
            Assert.AreEqual(expectedCount, actualCount);

            productsPage.OpenCart();

            string expectedTitle = TestContext.DataRow["product1"].ToString();
            string actualTitle = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            BasePage.Driver.Navigate().Back();
            Thread.Sleep(2000);
            productsPage.OpenCart();

            string expectedTitle1 = TestContext.DataRow["product1"].ToString();
            string actualTitle1 = cartPage.GetCartTitle();
            Assert.AreEqual(expectedTitle, actualTitle);

            exChildTest.Pass("Cart opened multiple times successfully");
        }
    }
}
