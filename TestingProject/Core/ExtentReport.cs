using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace courseproject.Core
{
    public class ExtentReport
    {
        public static ExtentReports extentReports;
        public static ExtentTest exParentTest;
        public static ExtentTest exChildTest;
        public static string dirpath;
        

        public static void CreateReport(string dirpath)
        {
            extentReports = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(dirpath);
            extentReports.AttachReporter(sparkReporter);
        }
    }
}