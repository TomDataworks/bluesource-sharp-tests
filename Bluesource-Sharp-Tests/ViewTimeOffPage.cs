using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class ViewTimeOffPage : BaseWebPage {
		[FindsBy(How = How.ClassName, Using = "vacation-summary-table")]
		private IWebElement vacation_summary;

		public ViewTimeOffPage ( IWebDriver driver ) : base (driver) {}

		public TimeOff.TimeOffLimits GetTimeOffLimits() {
			TimeOff.TimeOffLimits limits = new TimeOff.TimeOffLimits ();
			limits.sick =  float.Parse( vacation_summary.FindElement (By.XPath("span[2]")).Text.Split('/')[1].Split(' ')[0] );
			limits.vacation = float.Parse( vacation_summary.FindElement (By.XPath ("span[3]")).Text.Split('/')[1].Split(' ')[0] );
			limits.floating = float.Parse( vacation_summary.FindElement (By.XPath("span[4]")).Text.Split('/')[1].Split(' ')[0] );
			return limits;
		}

		public TimeOff.TimeOffUsed GetTimeOffUsed() {
			TimeOff.TimeOffUsed used = new TimeOff.TimeOffUsed ();
			//Console.WriteLine (vacation_summary.FindElement(By.XPath("span[position() > 1]")).Text);
			used.sick =  float.Parse( vacation_summary.FindElement (By.XPath("span[2]")).Text.Split('/')[0] );
			used.vacation = float.Parse( vacation_summary.FindElement (By.XPath ("span[3]")).Text.Split('/')[0] );
			used.floating = float.Parse( vacation_summary.FindElement (By.XPath("span[4]")).Text.Split('/')[0] );
			return used;
		}

		public EmployeeDataPage GoBack() {
			driver.Navigate ().Back ();
			return new EmployeeDataPage (driver);
		}
	}
}