using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace JoePizza_Testing_S.StepDefinitions
{
    [Binding]
    public class PlaceOrderStepDefinitions
    {
        private IWebDriver driver;
        [Given(@"the user is on the ""([^""]*)"" page")]
        public void GivenTheUserIsOnThePage(string checkout)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:7154/";
            Thread.Sleep(1000);
            driver.FindElement(By.Id("cart")).Click();
            IWebElement cpage = driver.FindElement(By.Id("checkout"));
            Assert.AreEqual(checkout, cpage.Text);
        }

        [When(@"the user clicks on ""([^""]*)""")]
        public void WhenTheUserClicksOn(string p0)
        {
            IWebElement order = driver.FindElement(By.Id("order"));
            string buttonValue = order.GetAttribute("value");
            Assert.AreEqual(p0, buttonValue);
            //order.Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", order);
        }

        [Then(@"the ""([^""]*)"" page should be displayed")]
        public void ThenThePageShouldBeDisplayed(string p0)
        {
            IWebElement orderc = driver.FindElement(By.Id("orderconfirmation"));
            Assert.AreEqual(p0 , orderc.Text);
        }

        [Then(@"the user should see the ""([^""]*)"" and ""([^""]*)""")]
        public void ThenTheUserShouldSeeTheAnd(string orderid, string amount)
        {
            IWebElement oid = driver.FindElement(By.Id(orderid));
            IWebElement am = driver.FindElement(By.Id(amount));
            Assert.IsNotNull(oid);
            Assert.IsNotNull(am);
            Console.WriteLine(oid.Text+" "+am.Text);
        }


        [Then(@"the user should see the following pizza details in the page:")]
        public void ThenTheUserShouldSeeTheFollowingPizzaDetailsInThePage(Table table)
        {
            IWebElement orderDetailsTable = driver.FindElement(By.Id("orderTable"));
            IList<IWebElement> rows = orderDetailsTable.FindElements(By.TagName("tr"));
            List<List<string>> actualTableData = new List<List<string>>();
            foreach (var row in rows)
            {
                IList<IWebElement> columns = row.FindElements(By.TagName("td"));
                if(columns.Count > 0)
                {
                    List<string> rowData = columns.Select(column => column.Text).ToList();
                    actualTableData.Add(rowData);
                }
                
            }
            List<List<string>> expectedTableData = table.Rows.Select(row => row.Values.ToList()).ToList();
            
            
            Assert.AreEqual(expectedTableData, actualTableData);

        }
        

    }
}
