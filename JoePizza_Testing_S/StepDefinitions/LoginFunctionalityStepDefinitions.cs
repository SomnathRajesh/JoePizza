using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class LoginFunctionalityStepDefinitions
    {
        private IWebDriver driver;

        [Given(@"The user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:7154/Identity/Account/Login";
            Thread.Sleep(1000);
        }

        [When(@"user enters the email ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenUserEntersTheEmailAndPassword(string p0, string p1)
        {
            driver.FindElement(By.Name("Input.Email")).SendKeys(p0);
            driver.FindElement(By.Name("Input.Password")).SendKeys(p1);
        }

        [When(@"user clicks the ""([^""]*)"" button")]
        public void WhenUserClicksTheButton(string p0)
        {
            IWebElement login = driver.FindElement(By.Id("login-submit"));
            Assert.AreEqual(p0, login.Text);
            login.Click();
        }

        [Then(@"user should be redirected to the pizza home page")]
        public void ThenUserShouldBeRedirectedToThePizzaHomePage()
        {
            Assert.IsTrue(driver.FindElement(By.Id("pizza-page")).Displayed);
            driver.Quit();

        }
    }
}
