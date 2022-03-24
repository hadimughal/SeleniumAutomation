using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFBTest
{
    public class BaseClass
    {
        protected OpenQA.Selenium.IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            webDriver = new ChromeDriver(Environment.CurrentDirectory + "\\Drivers", options);
        }

        [TearDown]
        public void CleanUp()
        {
            webDriver.Dispose();
            webDriver.Quit();
        }

    }
}
