using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;

namespace courseproject.Core
{
    [TestClass]
    public class BasePage
    {
        public static IWebDriver Driver;

        public void SeleniumInit()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

            ChromeOptions options = new ChromeOptions();

         
            options.AddArgument("--incognito");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-features=PasswordLeakDetection");
            options.AddArgument("--disable-features=AutofillServerCommunication");
            options.AddArgument("--no-default-browser-check");
            options.AddArgument("--no-first-run");

            string profilePath = Path.Combine(Path.GetTempPath(), "selenium_profile");
            options.AddArgument($"--user-data-dir={profilePath}");

            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            Driver = new ChromeDriver(options);
            Driver.Manage().Window.Maximize();
        }

        public void DriverClose()
        {
            Driver?.Quit();
            Driver = null;
        }

        public static void TakeScreenShot(Status status, string stepDetail)
        {
            string path = @"C:\Users\User\source\repos\TestingProject\TestingProject\ExtentReports\images\"
                          + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            File.WriteAllBytes(path, screenshot.AsByteArray);

            ExtentReport.exChildTest.Log(
                status,
                stepDetail,
                MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build()
            );
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            string reportPath = @"C:\Users\User\source\repos\TestingProject\TestingProject\ExtentReports\TestExecLog_"
                                + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";

            ExtentReport.CreateReport(reportPath);
            Console.WriteLine("Assembly initialized: Report created");
        }

        [AssemblyCleanup]
        public static void AssemblyClean()
        {
            ExtentReport.extentReports.Flush();
            Console.WriteLine("Assembly cleanup: Report flushed");
        }
    }
}
