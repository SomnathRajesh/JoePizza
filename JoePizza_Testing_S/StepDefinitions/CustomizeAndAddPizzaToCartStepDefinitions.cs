using System;
using System.Xml.Linq;
using JoePizza.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class CustomizeAndAddPizzaToCartStepDefinitions
    {
        private IWebDriver driver;
        [Given(@"the user navigates to the pizza selection page")]
        public void GivenTheUserNavigatesToThePizzaSelectionPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:7154/";
            Thread.Sleep(2000);
        }

        [When(@"the user clicks the customize button for the pizza named ""([^""]*)""")]
        public void WhenTheUserClicksTheCustomizeButtonForThePizzaNamed(string p0)
        {
            IWebElement pizzaCard = driver.FindElement(By.XPath($"//h4[@id='{p0}']/ancestor::div[@class='card']"));
            IWebElement cusB = pizzaCard.FindElement(By.XPath($".//a[contains(text(), '{"Customize"}')]"));
            //IWebElement customize = driver.FindElement(By.Id($"Customize-{p0}"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", cusB);
        }


        

        [Then(@"the user should see the ""([^""]*)"" page with pizza details")]
        public void ThenTheUserShouldSeeThePageWithPizzaDetails(string customize)
        {
            IWebElement page = driver.FindElement(By.Id("customize"));
            Assert.AreEqual(customize, page.Text);
        }

        [When(@"the user selects a topping named ""([^""]*)""")]
        public void WhenTheUserSelectsAToppingNamed(string p0)
        {
            IWebElement dropdown = driver.FindElement(By.Name("topping"));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText(p0);
            IWebElement selectedOption = select.SelectedOption;
            string selectedText = selectedOption.Text;
            Console.WriteLine($"Selected Topping: {selectedText}");
        }

        [When(@"the user selects a pizza size named ""([^""]*)""")]
        public void WhenTheUserSelectsAPizzaSizeNamed(string medium)
        {
            IWebElement dropdown = driver.FindElement(By.Name("size"));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText(medium);
            IWebElement selectedOption = select.SelectedOption;
            string selectedText = selectedOption.Text;
            Console.WriteLine($"Selected Size: {selectedText}");
        }

        [When(@"the user specifies the quantity as ""([^""]*)""")]
        public void WhenTheUserSpecifiesTheQuantityAs(string p0)
        {
            IWebElement q = driver.FindElement(By.Name("quantity"));
            q.SendKeys(p0);
        }

        [When(@"the user clicks ""([^""]*)""")]
        public void WhenTheUserClicks(string p0)
        {
            driver.FindElement(By.Id(p0)).Click();
        }

        [Then(@"the ""([^""]*)"" pizza with topping ""([^""]*)"" size ""([^""]*)"" and quantity ""([^""]*)"" should be added to the cart")]
        public void ThenThePizzaWithToppingSizeAndQuantityShouldBeAddedToTheCart(string p0, string p1, string p2, string p3)
        {
            driver.FindElement(By.Id("cart")).Click();
            IWebElement pizzaName = driver.FindElement(By.Id($"name-{p0}"));
            IWebElement tName = driver.FindElement(By.Id($"toppingname-{p0}"));
            IWebElement psize = driver.FindElement(By.Id($"psize-{p0}"));
            IWebElement q = driver.FindElement(By.Id($"quantity-{p0}"));
            Console.WriteLine(pizzaName.Text + " " + tName.Text + " " + psize.Text + " " + q.Text);
            Assert.AreEqual(p0, pizzaName.Text);
            Assert.AreEqual(p1, tName.Text);
            Assert.AreEqual(p2, psize.Text);
            Assert.AreEqual(p3, q.Text);
        }
        
        [When(@"the user clicks the customize button for another pizza named ""([^""]*)""")]
        public void WhenTheUserClicksTheCustomizeButtonForAnotherPizzaNamed(string p0)
        {
            driver.FindElement(By.Id("home")).Click();
            IWebElement pizzaCard = driver.FindElement(By.XPath($"//h4[@id='{p0}']/ancestor::div[@class='card']"));
            IWebElement cusB = pizzaCard.FindElement(By.XPath($".//a[contains(text(), '{"Customize"}')]"));
            //IWebElement customize = driver.FindElement(By.Id($"Customize-{p0}"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", cusB);
        }



    }
}
