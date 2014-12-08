using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;

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
			string start_s = start.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string end_s = end.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);

			start_date.Clear ();
			start_date.SendKeys (start_s);
			end_date.Clear ();
			end_date.SendKeys (end_s);
			vacation_type.FindElement (By.XPath ("//option[contains(., \"" + type + "\")]")).Click();
			start_date.Submit ();
			return new ManageTimeOffPage (driver);
		}

		public IWebElement GetVacationInfo( DateTime start ) {
			string start_s = start.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);
			return driver.FindElement (By.XPath ("//input[@value = '" + start_s + "']"));
		}

		public IWebElement TrashVacationInfo( DateTime start ) {
			string start_s = start.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);
			driver.FindElement (By.XPath ("//input[@value = '" + start_s + "']/../..//button[contains(. , 'Remove')]")).Click ();
		}

		public IReadOnlyCollection<IWebElement> GetAllTimeOff() {
			return driver.FindElements (By.XPath ("//tr/td/span"));
		}
	}
}