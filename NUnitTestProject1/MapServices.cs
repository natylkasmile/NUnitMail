using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitTestProject1
{
    public static class MapServices
    {
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
    }


}
