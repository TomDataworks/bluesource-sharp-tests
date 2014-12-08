using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace BluesourceSharpTests
{
	public class ManageTimeOffPage : BaseWebPage
	{
		[FindsBy(How = How.Name, Using = "new[vacation][start_date]")]
		private IWebElement start_date;

		[FindsBy(How = How.Name, Using = "new[vacation][end_date]")]
		private IWebElement end_date;

		[FindsBy(How = How.Name, Using = "new[vacation][vacation_type]")]
		private IWebElement vacation_type;

		public ManageTimeOffPage ( IWebDriver driver ) : base (driver) {}

		public ManageTimeOffPage SetVacationInfo( DateTime start, DateTime end, string type ) {
			SyncElement (By.Name ("new[vacation][start_date]"));
			//start_date.SendKeys (start.ToString ("yyyyMMdd"));
			//end_date.SendKeys (end.ToString ("yyyyMMdd"));
			start_date.Clear ();
			start_date.SendKeys ("2014-12-26");
			end_date.Clear ();
			end_date.SendKeys ("2014-12-27");
			vacation_type.FindElement (By.XPath ("//option[contains(., \"" + type + "\")]")).Click();
			start_date.Submit ();
			return new ManageTimeOffPage (driver);
		}

		public IReadOnlyCollection<IWebElement> GetAllTimeOff() {
			return driver.FindElements (By.XPath ("//tr/td/span"));
		}
	}
}