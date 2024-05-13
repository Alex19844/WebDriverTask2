using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Net.Http;
using Selenium.WebDriver.UndetectedChromeDriver;


namespace WebDriverTask2
{
    public class Tests
    {
        private IWebDriver driver { get; set; }
        private const string url = "https://pastebin.com/";
        private const string loginURL = "https://pastebin.com/6b6EBsas";
        private const string expirationValue = "10 Minutes";
        private const string syntaxValue = "Bash";
        private const string nameValue = "how to gain dominance among developers";
        private const string codeValue = @"
        git config --global user.name ""New Sheriff in Town""
        git reset $(git commit-tree HEAD^{tree} -m ""Legacy code"")
        git push origin master --force
        ";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // Set implicit wait for elements
        }

        [Test]
        public void WebDriverTest()
        {
            // Navigate to pastebin.com
            driver.Navigate().GoToUrl(url);

            // Delay for pop-up window to show
            //System.Threading.Thread.Sleep(2000);

            // Find the 'Agree' button on the pop-up window and click on it (if using a VPN).
            //IWebElement agreeButton = driver.FindElement(By.CssSelector("button.css-47sehv"));
            //agreeButton.Click();

            // Login to website - couldn't bypass Cloudflare security
            //IWebElement loginLink = driver.FindElement(By.CssSelector("a.btn-sign.sign-in"));
            //loginLink.Click();

            // Find 'NewPaste' button and click on it
            IWebElement newpasteElement = driver.FindElement(By.CssSelector("a.header__btn"));
            newpasteElement.Click();

            // Find 'New Paste' form and enter the text
            driver.FindElement(By.Id("postform-text")).SendKeys(codeValue);

            // Scroll down the window
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,600)", "");

            // Find 'Syntax Highlighting' drop-down list and click on it
            var dropdownElementSH = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-format')]//span"));
            dropdownElementSH.Click();

            // Find 'Bash' option and click on it
            var dropdownOptionsSH = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option']"));
            dropdownOptionsSH.FirstOrDefault(x => x.Text.Equals(syntaxValue)).Click();


            // Find drop-down list 'Paste Expiration' and click on it
            var dropdownElementPE = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-expiration')]//span"));
            dropdownElementPE.Click();

            // Find '10 Minutes' option and click on it
            var dropdownOptionsPE = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option']"));
            dropdownOptionsPE.FirstOrDefault(x => x.Text.Equals(expirationValue)).Click();

            // Find 'PasteName/Title' form and enter the text
            driver.FindElement(By.Id("postform-name")).SendKeys(nameValue);

            // Scroll up the window
            var js1 = (IJavaScriptExecutor)driver;
            js1.ExecuteScript("window.scrollBy(0,-600)", "");

            // Find 'Syntax Highlighting' toggle button and click on it
            var checkbox = driver.FindElement(By.ClassName("toggle__control"));
            checkbox.Click();

            //driver.Navigate().GoToUrl(loginURL); // redirect to URL with login user

            // Scroll down the window
            var js2 = (IJavaScriptExecutor)driver;
            js2.ExecuteScript("window.scrollBy(0,600)", "");

            // Find 'Create New Paste' button and click on it
            //var createPaste = driver.FindElement(By.CssSelector("button[class='btn -big']"));
            var createPaste = driver.FindElement(By.CssSelector("button.btn.-big"));
            createPaste.Click();

            // Find 'info-top' element
            IWebElement infotopElement = driver.FindElement(By.ClassName("info-top"));

            // Assert that browser page title matches 'Paste Name/Title'
            Assert.That(infotopElement.Text.Equals(nameValue));

            // Find 'Syntax Highlighting' element
            IWebElement syntaxElement = driver.FindElement(By.CssSelector("a.btn.-small.h_800"));

            // Assert that syntax is suspended for bash
            Assert.That(syntaxElement.Text.Equals(syntaxValue));

            // Find 'Paste Data' element
            IWebElement dataElement = driver.FindElement(By.CssSelector("div.source.bash"));
            string actualData = dataElement.Text;

            // Assert that code matches
            string expectedData = "          git config --global user.name \"New Sheriff in Town\"\r\n        git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\n        git push origin master --force\r\n        ";
            Assert.That(actualData, Is.EqualTo(expectedData));

            Assert.Pass(); // Instruction for Breakpoint
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}