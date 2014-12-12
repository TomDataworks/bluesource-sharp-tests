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

		[FindsBy(How = How.Id, Using = "panel_body_3")]
		private IWebElement vacation_summary;

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

		public TimeOff.TimeOffLimits GetTimeOffLimits() {
			TimeOff.TimeOffLimits used = new TimeOff.TimeOffLimits ();
			//Console.WriteLine (vacation_summary.FindElement(By.XPath("span[position() > 1]")).Text);
			used.sick =  float.Parse( vacation_summary.FindElement (By.XPath("div/table/tbody/tr[1]/td[2]")).Text.Split('/')[1] );
			used.vacation = float.Parse( vacation_summary.FindElement (By.XPath ("div/table/tbody/tr[2]/td[2]")).Text.Split('/')[1] );
			used.floating = float.Parse( vacation_summary.FindElement (By.XPath("div/table/tbody/tr[3]/td[2]")).Text.Split('/')[1] );
			return used;
		}

		public TimeOff.TimeOffUsed GetTimeOffUsed() {
			TimeOff.TimeOffUsed limits = new TimeOff.TimeOffUsed ();
			//Console.WriteLine (vacation_summary.FindElement(By.XPath("span[position() > 1]")).Text);
			limits.sick =  float.Parse( vacation_summary.FindElement (By.XPath("div/table/tbody/tr[1]/td[2]")).Text.Split('/')[0] );
			limits.vacation = float.Parse( vacation_summary.FindElement (By.XPath ("div/table/tbody/tr[2]/td[2]")).Text.Split('/')[0] );
			limits.floating = float.Parse( vacation_summary.FindElement (By.XPath("div/table/tbody/tr[3]/td[2]")).Text.Split('/')[0] );
			return limits;
		}
	}
}