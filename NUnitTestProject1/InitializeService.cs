using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using NUnit.Allure;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;

namespace NUnitTestProject1
{
    public class InitializeService
    {
        protected IWebDriver driver;
        protected string mailAdress = "natashamailtest@yandex.ru";
        protected string mailPass = "123123!";
        protected string findMailAdress = "";
        protected string countLetters = "";
        protected int countSpamLetters = 0;
        protected string host_chrome = "localhost:14572";
        protected string host_ff = "localhost:5557";

        public void InitBrowser(string Selenium_grid = "default")
        {
            switch (Selenium_grid)
            {
                case "chrome":

                    //for selenium grid
                    ChromeOptions options = new ChromeOptions();
                    var remoteAddress = new Uri(string.Format("http://{0}/wd/hub", host_chrome));
                    driver = new RemoteWebDriver(remoteAddress, options);
                    break;
                case "firefox":

                    //for selenium grid
                    FirefoxOptions FFoptions = new FirefoxOptions();
                    remoteAddress = new Uri(string.Format("http://{0}/wd/hub", host_ff));
                    driver = new RemoteWebDriver(remoteAddress, FFoptions);
                    break;
                default:
                    //for chrome
                    driver = new ChromeDriver(".");
                    break;
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(20);
         
        }
        

        public void SetfindMailAdress(string _findMailAdress)
        {
            findMailAdress = _findMailAdress;
        }

        public void GoToUrl(string url = "")
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
        }

        public void Autorize()
        {
            driver.FindElement(By.Id("passp-field-login")).SendKeys(mailAdress);
            driver.FindElement(By.CssSelector(".passp-sign-in-button [type=\"submit\"]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("passp-field-passwd")).SendKeys(mailPass);
            driver.FindElement(By.CssSelector(".passp-sign-in-button [type=\"submit\"]")).Click();
            Thread.Sleep(2000);
        }

        public void FindMails()
        {
            GoToUrl("https://mail.yandex.ru");
            driver.FindElement(By.CssSelector(".search-input input")).Click();
            driver.FindElement(By.CssSelector(".search-input input")).SendKeys(findMailAdress + " И папка:Входящие");
            driver.FindElement(By.CssSelector(".search-input__form-button")).Click();
            Thread.Sleep(2000);
            countLetters = driver.FindElement(By.CssSelector(".mail-MessagesSearchInfo-Title_misc")).Text;
        }

        public void FindSpamMails()
        {
            GoToUrl("https://mail.yandex.ru#spam");
            IList<IWebElement> SpamLetters = driver.FindElements(By.CssSelector(".mail-MessageSnippet-FromText"));
            foreach (IWebElement elem in SpamLetters)
            {
                if (elem.GetAttribute("title").Equals(findMailAdress))
                    countSpamLetters++;
            }
        }
        public void SendMails()
        {
            GoToUrl("https://mail.yandex.ru#compose");
            driver.FindElement(By.Name("to")).SendKeys(findMailAdress);
            driver.FindElement(By.CssSelector("#cke_1_contents div")).Click();
            driver.FindElement(By.CssSelector("#cke_1_contents div")).SendKeys(report());

            driver.FindElement(By.CssSelector(".mail-Compose-ComplexSendButton")).Click();
            Thread.Sleep(2000);
        }
        private string report()
        {
                return ($"От пользователя {findMailAdress} найдено {countLetters}.");
        }

        [OneTimeTearDown]
        public void TeardownTest()
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
