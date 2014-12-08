using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class EmployeeDataPage : BaseWebPage
	{
		[FindsBy(How = How.XPath, Using = "//div/h4[contains(., \"Time Off Info\")]/../div/a[contains(., \"Manage\")]")]
		private IWebElement manageTimeOff;

		[FindsBy(How = How.XPath, Using = "//div/h4[contains(., \"Time Off Info\")]/../div/a[contains(., \"View\")]")]
		private IWebElement viewTimeOff;

		public EmployeeDataPage ( IWebDriver driver ) : base (driver) {}

		public ManageTimeOffPage GotoManageTimeOff() {
			// SyncElement (By.XPath ("//div/h4[contains(., \"Time Off Info\")]/../div/a[contains(., \"Manage\")]"));
			manageTimeOff.Click ();
			return new ManageTimeOffPage (driver);
		}

		public ViewTimeOffPage GotoViewTimeOff() {
			viewTimeOff.Click ();
			return new ViewTimeOffPage (driver);
		}
	}
}