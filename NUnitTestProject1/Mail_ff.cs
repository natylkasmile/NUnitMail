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
    [AllureSuite("MailTests in firefox")]
    [AllureDisplayIgnored]
    public class Mail_ff : InitializeService
    {
        string findMail = "natylkasmile@gmail.com";
        [SetUp]
        public void Setup()
        {
            InitBrowser("firefox");
            SetfindMailAdress(findMail);
        }

        [Test(Description = "find letters in ff")]
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
            }, "FindMails");
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                SendMails();
            }, "SendMails");
        }
    }
}
