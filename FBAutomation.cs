using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Linq;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumFBTest
{
    [TestFixture]
    public class FBAutomation : BaseClass
    {
        [Test]
        [TestCase("elysiumdrive1@gmail.com", "q1w2e3r$")]
        public void Login_Valid(string username, string password)
        {
            Console.WriteLine("Credentials used: " + username + " " + password);
            webDriver.Url = "https://www.facebook.com/";
            var input = webDriver.FindElement(By.Id("email"));
            input.SendKeys(username);
            input = webDriver.FindElement(By.Id("pass"));
            input.SendKeys(password);
            ClickAndWaitForPageToLoad(webDriver, By.Name("login"));
            String expectedURL = "https://www.facebook.com/";
            String currentURL = webDriver.Url;
            Assert.That(currentURL, Is.EqualTo(expectedURL));
            Console.WriteLine("Expected Result: User should successfully be logged in with valid credentials");
            Console.WriteLine("Actual Result: User successfully logged in");
            Console.WriteLine("TestStatus: PASS");
            Thread.Sleep(2000);
        }

        [Test]
        [TestCase("elysiumdrive1@gmail.com", "q1w2e3r$")]
        public void LoginWithBackNavigation(string username, string password)
        {
            Console.WriteLine("Credentials used: " + username + " " + password);
            webDriver.Url = "https://www.facebook.com/";
            var input = webDriver.FindElement(By.Id("email"));
            input.SendKeys(username);
            input = webDriver.FindElement(By.Id("pass"));
            input.SendKeys(password); ClickAndWaitForPageToLoad(webDriver, By.Name("login"));
            String expectedURL = "https://www.facebook.com/";
            String currentURL = webDriver.Url;
            webDriver.Navigate().Back();
            String[] loggedIn = { "Log in to Facebook", "Facebook – log in or sign up" };
            Assert.Multiple(() =>
            {
                Assert.That(currentURL, Is.EqualTo(expectedURL));
                Assert.That(webDriver.Title, Has.No.Member(loggedIn));
            });
            Console.WriteLine("Expected Result: After successful login user login page should not appear if navigated back");
            Console.WriteLine("Actual Result: On navigation login screen didn't show up");
            Console.WriteLine("TestStatus: PASS");
            Thread.Sleep(2000);
        }

        [Test]
        [TestCase("elysiumsdsadasdasd1@gmail.com", "q1w2e3r$", TestName = "inValidEmail")]
        [TestCase("elysiumsdrive1@gmail.com", "1weqweqwe", TestName = "inValidPassword")]
        public void Login_Invalid(String email, String password)
        {
            Console.WriteLine("Credentials used: " + email + " " + password);
            webDriver.Url = "https://www.facebook.com/";
            var input = webDriver.FindElement(By.Id("email"));
            input.SendKeys(email);
            input = webDriver.FindElement(By.Id("pass"));
            input.SendKeys(password);
            ClickAndWaitForPageToLoad(webDriver, By.Name("login"));
            String currentTitle = webDriver.Title;
            Assert.IsTrue((currentTitle.Contains("Log in to Facebook") || currentTitle.Contains("Facebook – log in or sign up")) ? true : false, "Failed");
            Console.WriteLine("Expected Result: User should not be granted access with invalid credentials");
            Console.WriteLine("Actual Result: Access not granted with invalid credentials");
            Console.WriteLine("TestStatus: PASS");
            Thread.Sleep(2000);
        }
        private void ClickAndWaitForPageToLoad(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var elements = driver.FindElements(elementLocator);
                if (elements.Count == 0)
                {
                    Console.WriteLine("NoSuchElemenetException");
                    throw new NoSuchElementException(
                        "No elements " + elementLocator + " ClickAndWaitForPageToLoad");
                }

                var element = elements.FirstOrDefault(e => e.Displayed);
                element.Click();
                wait.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }
    }
}