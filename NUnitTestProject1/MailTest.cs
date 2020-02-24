using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using NUnit.Allure;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;

namespace NUnitTestProject1
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("MailTests")]
    [AllureDisplayIgnored]
    public class MailTest : InitializeService
    {
       // string findMail = "ilya.filinin@nordclan.com";
        string findMail = "natylkasmile@gmail.com";
        [SetUp]
        public void Setup()
        {
            InitBrowser(true);
            SetfindMailAdress(findMail);
        }

        [Test(Description = "find letters")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Nata")]
        public void FindLettersTest()
        {
            GoToUrl("https://passport.yandex.ru/auth");
            Thread.Sleep(2000);
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                Autorize();
            }, "Autorize");

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                FindMails();
            }, $"FindMails from {findMail}");

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                SendMails();
            }, $"SendMail to {findMail}");

            Assert.Pass();
        }
    }
}