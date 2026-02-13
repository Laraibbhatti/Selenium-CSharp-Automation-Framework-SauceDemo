using courseproject.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace courseproject.Login
{
    [TestClass]
    public class LoginTest : ExtentReport
    {
        LoginPage loginPage = new LoginPage();
        BasePage basePage = new BasePage();

        public TestContext TestContext { get; set; }

        #region Initialization and Cleanup

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("LoginTest class initialization");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("LoginTest class cleanup");
        }

        [TestInitialize]
        public void TestInit()
        {
            Console.WriteLine("LoginTest initialization");
            basePage.SeleniumInit();
            exParentTest = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("LoginTest cleanup");
            basePage.DriverClose();
            extentReports.Flush();
        }

        #endregion

        // ---------- TEST CASE 001: Positive login ----------
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_001_validEmailValidPassword_positive", DataAccessMethod.Sequential)]
        public void TestCase_001_validEmailValidPassword_positive()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Positive Login");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedProduct = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, password);

            string actualTitle = loginPage.GetProductTitle();
            Assert.AreEqual(expectedProduct, actualTitle);

            exChildTest.Pass("Login successful with valid credentials");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_002_invalidPassword", DataAccessMethod.Sequential)]
        public void TestCase_002_invalidPassword()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Invalid Password");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, password);

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info($"Attempted login with invalid password: {password}");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_003_invalidUsername", DataAccessMethod.Sequential)]
        public void TestCase_003_invalidUsername()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Invalid Username");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, password);

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info($"Attempted login with invalid username: {username}");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_004_emptyCredentials", DataAccessMethod.Sequential)]
        public void TestCase_004_emptyCredentials()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Empty Credentials");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, password);

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info("Attempted login with empty credentials");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_005_passwordCaseSensitivity", DataAccessMethod.Sequential)]
        public void TestCase_005_passwordCaseSensitivity()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Password Case Sensitivity");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, password);

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info("Attempted login with password case variation");
        }

        // ---------- TEST CASE 006: Blank password ----------
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_006_blankPassword", DataAccessMethod.Sequential)]
        public void TestCase_006_blankPassword()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Blank Password");

            string url = TestContext.DataRow["url"].ToString();
            string username = TestContext.DataRow["username"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, username, "");

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info("Attempted login with blank password");
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Datacourse.xml", "TestCase_007_blankUsername", DataAccessMethod.Sequential)]
        public void TestCase_007_blankUsername()
        {
            exChildTest = exParentTest.CreateNode("Login Page - Blank Username");

            string url = TestContext.DataRow["url"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            string expectedError = TestContext.DataRow["product"].ToString();

            loginPage.Login(url, "", password);

            string actualError = loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError);

            exChildTest.Info("Attempted login with blank username");
        }
    }
}
