using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lecture5_HomeTask
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver = GetDriver();

        [OneTimeSetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void AfterTest()
        {
            driver.Quit();
        }

        #region
        [Test]
        public void TestMethod1()
        {
            string url = "https://www.demoblaze.com/index.html";
            driver.Navigate().GoToUrl(url);

            IWebElement loginButton = driver.FindElement(By.Id("login2"));
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='loginusername']")));
            IWebElement usernameField = driver.FindElement(By.XPath("//input[@id='loginusername']"));
            usernameField.Click();
            usernameField.Clear();
            usernameField.SendKeys("Dmytro Zubenko");
           
            IWebElement passwordField = driver.FindElement(By.XPath("//input[@id='loginpassword']"));
            passwordField.Click();
            passwordField.Clear();
            passwordField.SendKeys("QA Automation Student1234");
            IWebElement LogIn = driver.FindElement(By.XPath("//button[@onclick='logIn()']")); 
            LogIn.Click();
           
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait1.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='logout2']")));
            Assert.AreEqual("Welcome Dmytro Zubenko", driver.FindElement(By.XPath("//a[@id='nameofuser']")).Text, "Title text differs from expected!");
        }
        #endregion

        #region
        [TestCase("JohnDoe", "passw0rd")]
        [TestCase("LiliaJY","passw0rd")]
        [TestCase("GoingTo","BeAuto!")]
        public void TestMethod2(string name, string password)
        {
            string url = "http://automationpractice.com";
            driver.Navigate().GoToUrl(url);

            IWebElement SingInButton = driver.FindElement(By.ClassName("login"));
            SingInButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            IWebElement mailField = driver.FindElement(By.Id("email"));
            mailField.Click();
            mailField.SendKeys(name);

            IWebElement passwordField = driver.FindElement(By.Id("passwd"));
            passwordField.Click();
            passwordField.SendKeys(password);

            IWebElement loginbutton = driver.FindElement(By.Id("SubmitLogin"));
            loginbutton.Click();

            Assert.AreEqual("There is 1 error", driver.FindElement(By.XPath("//p[text()='There is 1 error']")).Text, "Title text differs from expected!");
        }

        #endregion

        #region
        public static IEnumerable<TestCaseData> LogIn_credentials
        {
            get
            {
                yield return new TestCaseData("JohnDoe","passw0rd");
                yield return new TestCaseData("LiliaJY","isNotMe");
                yield return new TestCaseData("GoingTo","BeAuto!");
            }
        }

        [TestCaseSource("LogIn_credentials")]

        public void TestMethod4(string mail, string password)
        {
            string url = "http://automationpractice.com";
            driver.Navigate().GoToUrl(url);

            IWebElement SingInButton = driver.FindElement(By.XPath("//a[@class='login']"));
            SingInButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            IWebElement mailField = driver.FindElement(By.Id("email"));
            mailField.Click();
            mailField.SendKeys(mail);

            IWebElement passwordField = driver.FindElement(By.Id("passwd"));
            passwordField.Click();
            passwordField.SendKeys(password);

            IWebElement loginbutton = driver.FindElement(By.Id("SubmitLogin"));
            loginbutton.Click();

            Assert.AreEqual("There is 1 error", driver.FindElement(By.XPath("//p[text()='There is 1 error']")).Text, "Title text differs from expected!");
        }
        #endregion

        private static IWebDriver GetDriver()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(directory);
        }
    }
}