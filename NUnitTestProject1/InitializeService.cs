using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Web;
using System.Web;
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

        #region Map
        public static By login = By.Id("passp-field-login");
        public static By password = By.Id("passp-field-passwd");
        public static By btnSubmit = By.CssSelector(".passp-sign-in-button [type=\"submit\"]");
        public static By inputSearch = By.CssSelector(".search-input input");
        public static By btnSearch = By.CssSelector(".search-input__form-button");
        public static By counterLetters = By.CssSelector(".mail-MessagesSearchInfo-Title_misc");
        public static By spamRow = By.CssSelector(".mail-MessageSnippet-FromText");
        public static By msngTo = By.Name("to");
        public static By msngText = By.CssSelector("#cke_1_contents div");
        public static By btnMsngSend = By.CssSelector(".mail-Compose-ComplexSendButton");
        #endregion Map

        protected string mailAdress = ConfigurationManager.AppSettings.Get("mailAdress");
        protected string mailPass = ConfigurationManager.AppSettings.Get("mailPass");
        protected string findMailAdress = "";
        protected string countLetters = "";
        protected int countSpamLetters = 0; 


        public void InitBrowser(string Selenium_grid = "default")
        {
            switch (Selenium_grid)
            {
                case "chrome":
                    //for selenium grid chrome
                    ChromeOptions options = new ChromeOptions();
                    var remoteAddress = new Uri(string.Format("http://{0}/wd/hub", ConfigurationManager.AppSettings.Get("host_chrome")));
                    driver = new RemoteWebDriver(remoteAddress, options);
                    break;
                case "firefox":
                    //for selenium grid firefox
                    FirefoxOptions FFoptions = new FirefoxOptions();
                    remoteAddress = new Uri(string.Format("http://{0}/wd/hub", ConfigurationManager.AppSettings.Get("host_ff")));
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
            Thread.Sleep(2000);
        }

        public void Autorize()
        {
            GoToUrl("https://passport.yandex.ru/auth");
            Thread.Sleep(2000);
            driver.FindElement(login).SendKeys(mailAdress);
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(2000);
            driver.FindElement(password).SendKeys(mailPass);
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(2000);
        }

        public void FindMails()
        {
            GoToUrl("https://mail.yandex.ru");
            driver.FindElement(inputSearch).Click();
            driver.FindElement(inputSearch).SendKeys(findMailAdress + " И папка:Входящие");
            driver.FindElement(btnSearch).Click();
            Thread.Sleep(2000);
            countLetters = driver.FindElement(counterLetters).Text;
        }

        public void FindSpamMails()
        {
            GoToUrl("https://mail.yandex.ru#spam");
            IList<IWebElement> SpamLetters = driver.FindElements(spamRow);
            foreach (IWebElement elem in SpamLetters)
            {
                if (elem.GetAttribute("title").Equals(findMailAdress))
                    countSpamLetters++;
            }
        }
        public void SendMails()
        {
            GoToUrl("https://mail.yandex.ru#compose");
            driver.FindElement(msngTo).SendKeys(findMailAdress);
            driver.FindElement(msngText).Click();
            driver.FindElement(msngText).SendKeys(report());

            driver.FindElement(btnMsngSend).Click();
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
