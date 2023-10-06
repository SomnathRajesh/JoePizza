using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class RemoveFromCartStepDefinitions
    {
        private IWebDriver driver;
        [Given(@"the user navigates to the ""([^""]*)"" page")]
        public void GivenTheUserNavigatesToThePage(string checkout)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:7154/";
            Thread.Sleep(1000);
            driver.FindElement(By.Id("cart")).Click();
            IWebElement cpage = driver.FindElement(By.Id("checkout"));
            Assert.AreEqual(checkout, cpage.Text);
        }


        [When(@"the user clicks on the trash icon for ""([^""]*)"" pizza")]
        public void WhenTheUserClicksOnTheTrashIconForPizza(string p0)
        {
           IWebElement removePizza = driver.FindElement(By.Id($"pizzarow-{p0}"));
            removePizza.FindElement(By.Id("remove")).Click();
        }

        [Then(@"the ""([^""]*)"" pizza is removed from the cart")]
        public void ThenThePizzaIsRemovedFromTheCart(string p0)
        {
            try
            {
                IWebElement pizzaElement = driver.FindElement(By.Id($"pizzarow-{p0}"));
                Assert.Fail($"Pizza with name '{p0}' should not be present in the cart.");
            }
            catch (NoSuchElementException)
            {
                
            }
        }
        

    }
}
