using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class UserRegistrationStepDefinitions
    {
        private IWebDriver driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Given(@"user is on the registration page")]
        public void GivenUserIsOnTheRegistrationPage()
        {
            driver.Url = "https://localhost:7154/Identity/Account/Register";
        }

        [When(@"user enters email ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenUserEntersEmailAndPassword(string p0, string p1)
        {
            driver.FindElement(By.Name("Input.Email")).SendKeys(p0);
            driver.FindElement(By.Name("Input.Password")).SendKeys(p1);
        }

        [When(@"user confirms password ""([^""]*)""")]
        public void WhenUserConfirmsPassword(string p0)
        {
            driver.FindElement(By.Name("Input.ConfirmPassword")).SendKeys(p0);
        }
    

        [When(@"user clicks on the ""([^""]*)"" button")]
        public void WhenUserClicksOnTheButton(string register)
        {
            
            IWebElement reg = driver.FindElement(By.Id("registerSubmit"));
            Assert.AreEqual(register, reg.Text);
            reg.Click();
        }

        [Then(@"user should be redirected to the pizza page")]
        public void ThenUserShouldBeRedirectedToThePizzaPage()
        {
            Assert.IsTrue(driver.FindElement(By.Id("pizza-page")).Displayed);
            driver.Quit();
        }

    }
}
