using courseproject.Core;
using courseproject.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace courseproject.Products
{
    [TestClass]
    public class ProductsTest : ExtentReport
    {
        LoginPage loginPage = new LoginPage();
        ProductsPage productsPage = new ProductsPage();
        BasePage basePage = new BasePage();

        public TestContext TestContext { get; set; }

        #region Initialization & Cleanup

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ProductsTest class initialization");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("ProductsTest class cleanup");
        }

        [TestInitialize]
        public void TestInit()
        {
            Console.WriteLine("ProductsTest initialization");
            basePage.SeleniumInit();
            exParentTest = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("ProductsTest cleanup");
            basePage.DriverClose();
            extentReports.Flush();
        }

        #endregion

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_001_AddSingleProduct", DataAccessMethod.Sequential)]
        public void ProductsTestCase_001_AddSingleProduct()
        {
            exChildTest = exParentTest.CreateNode("Add Single Product");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();

            string expected = TestContext.DataRow["product"].ToString();
            string actual = productsPage.GetCartCount();

            Assert.AreEqual(expected, actual);
            exChildTest.Pass("Single product added successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_002_AddMultipleProducts", DataAccessMethod.Sequential)]
        public void ProductsTestCase_002_AddMultipleProducts()
        {
            exChildTest = exParentTest.CreateNode("Add Multiple Products");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddMultipleProducts();

            string expected = TestContext.DataRow["product"].ToString();
            string actual = productsPage.GetCartCount();

            Assert.AreEqual(expected, actual);
            exChildTest.Pass("Multiple products added successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_003_RemoveProduct", DataAccessMethod.Sequential)]
        public void ProductsTestCase_003_RemoveProduct()
        {
            exChildTest = exParentTest.CreateNode("Remove Product");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();
            productsPage.RemoveBackpack();

            Assert.IsTrue(productsPage.IsCartEmpty());
            exChildTest.Pass("Product removed and cart is empty");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_004_OpenCart", DataAccessMethod.Sequential)]
        public void ProductsTestCase_004_OpenCart()
        {
            exChildTest = exParentTest.CreateNode("Open Cart");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();
            productsPage.OpenCart();

            bool actual = BasePage.Driver.Url.Contains("cart");
            Assert.IsTrue(actual);
            exChildTest.Pass("Cart opened successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_005_SortProducts", DataAccessMethod.Sequential)]
        public void ProductsTestCase_005_SortProducts()
        {
            exChildTest = exParentTest.CreateNode("Sort Products");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.SortProducts(1); 

            string expected = TestContext.DataRow["product"].ToString();
            string actual = productsPage.GetSelectedSortOption();

            Assert.AreEqual(expected, actual);
            exChildTest.Pass("Products sorted successfully");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "Datacourse.xml", "ProductsTestCase_006_VerifyCartCount", DataAccessMethod.Sequential)]
        public void ProductsTestCase_006_VerifyCartCount()
        {
            exChildTest = exParentTest.CreateNode("Verify Cart Count");

            loginPage.Login(
                TestContext.DataRow["url"].ToString(),
                TestContext.DataRow["username"].ToString(),
                TestContext.DataRow["password"].ToString());

            productsPage.AddBackpackToCart();
            productsPage.AddBikeLightToCart();

            string expected = TestContext.DataRow["product"].ToString();
            string actual = productsPage.GetCartCount();

            Assert.AreEqual(expected, actual);
            exChildTest.Pass("Cart count verified correctly");
        }
    }
}
