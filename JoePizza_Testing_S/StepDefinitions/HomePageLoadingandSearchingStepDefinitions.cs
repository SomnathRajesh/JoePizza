using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class HomePageLoadingandSearchingStepDefinitions
    {
        private IWebDriver driver;
        
        [Given(@"the user opens the browser")]
        public void GivenTheUserOpensTheBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [When(@"the user enters the URL")]
        public void WhenTheUserEntersTheURL()
        {
            driver.Url = "https://localhost:7154/";
            Thread.Sleep(5000);
        }

        [Then(@"the pizza selection page should be displayed")]
        public void ThenThePizzaSelectionPageShouldBeDisplayed()
        {
            Assert.IsTrue(driver.FindElement(By.Id("pizza-page")).Displayed);
            Thread.Sleep(1000); 
        }

        [When(@"the user searches for ""([^""]*)"" Pizza")]
        public void WhenTheUserSearchesForPizza(string p0)
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys(p0);
            searchInput.Click();
            Thread.Sleep(1000);
        }

        [Then(@"the search results should include ""([^""]*)""")]
        public void ThenTheSearchResultsShouldInclude(string p0)
        {
            IWebElement searchResult = driver.FindElement(By.Id(p0));
            Assert.AreEqual(p0, searchResult.Text);
            Console.WriteLine(searchResult.Text);
            driver.Quit();
           
        }
        [Then(@"the search results should not include ""([^""]*)""")]
        public void ThenTheSearchResultsShouldNotInclude(string p0)
        {
            try
            {
                IWebElement pizzaElement = driver.FindElement(By.Id(p0));
                Assert.Fail($"Pizza with name '{p0}' should not be present in the page.");
            }
            catch (NoSuchElementException)
            {

            }
            driver.Quit();
        }

    }
}
