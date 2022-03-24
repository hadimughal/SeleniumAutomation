using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace SeleniumFBTest
{
    class Program
    {
        [Test]
      
        public static void Main()
        {
            var webDriver = LaunchBrowser();
            try
            {
                var fb_webdriver = new FBAutomation(webDriver);
                fb_webdriver.Login("elysiumdrive1@gmail.com", "q1w2e3r1");
                System.Threading.Thread.Sleep(10000);
                fb_webdriver.InvalidLogin("elysiumdrive1@gmail.com", "q1w2e3r");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing automation");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Success finally");
                Console.ReadKey();
                webDriver.Quit();
            }
        }

        static IWebDriver LaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
            var driver = new ChromeDriver(Environment.CurrentDirectory+"\\Drivers", options);
            return driver;
        }
    }
}
