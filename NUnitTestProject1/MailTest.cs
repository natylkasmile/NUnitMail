using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace NUnitTestProject1
{
    public class MailTest : InitializeService
    {
        [SetUp]
        public void Setup()
        {
            InitBrowser();
            SetfindMailAdress("natylkasmile@gmail.com");
        }

        [Test]
        public void FindLettersTest()
        {
            GoToUrl("https://passport.yandex.ru/auth");
            Thread.Sleep(2000);
            Autorize();
            FindMails();
           // FindSpamMails();
            SendMails();
            Assert.Pass();
        }
    }
}