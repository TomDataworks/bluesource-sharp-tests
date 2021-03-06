﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using OpenQA.Selenium.Interactions;

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

		[FindsBy(How = How.Name, Using = "new[vacation][reason]")]
		private IWebElement other_reason;

		[FindsBy(How = How.Id, Using = "new_vacation_vacation_type")]
		private IWebElement dropdown;

		[FindsBy(How = How.XPath, Using = "//label[contains(., 'Half Day')]")]
		private IWebElement vacation_half_day;

		[FindsBy(How = How.ClassName, Using = "vacation-summary-table")]
		private IWebElement vacation_summary;

		[FindsBy(How = How.LinkText, Using = "Back")]
		private IWebElement back_link;

		public ManageTimeOffPage ( IWebDriver driver ) : base (driver) {}

		public ManageTimeOffPage SetVacationInfo( DateTime start, DateTime end, string type, string reason, bool halfday ) {
			Actions mouse = new Actions(driver);

			SyncElement (By.Name ("new[vacation][start_date]"));
			string start_s = start.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);
			string end_s = end.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);

			start_date.Clear ();
			start_date.SendKeys (start_s);
			end_date.Clear ();
			end_date.SendKeys (end_s);
			vacation_type.FindElement (By.XPath ("//option[contains(., \"" + type + "\")]")).Click();
			if (!String.IsNullOrEmpty(reason)) {
				mouse.MoveToElement (dropdown).Build ().Perform ();
				other_reason.SendKeys (reason);
			}
			if (halfday) {
				vacation_half_day.Click ();
			}
			start_date.Submit ();
			return new ManageTimeOffPage (driver);
		}

		public IWebElement GetVacationInfo( DateTime start ) {
			string start_s = start.ToString ("yyyy-MM-dd", CultureInfo.InvariantCulture);
			return driver.FindElement (By.XPath ("//input[@value = '" + start_s + "']/../.."));
		}

		public void TrashVacationInfo( DateTime start ) {
			GetVacationInfo(start).FindElement (By.XPath("//a[@data-method = 'delete']")).Click();
			driver.SwitchTo ().Alert ().Accept ();
		}

		public IReadOnlyCollection<IWebElement> GetAllTimeOff() {
			return driver.FindElements (By.XPath ("//tr/td/span"));
		}

		public TimeOff.TimeOffLimits GetTimeOffLimits() {
			TimeOff.TimeOffLimits limits = new TimeOff.TimeOffLimits ();
			//Console.WriteLine (vacation_summary.FindElement(By.XPath("span[position() > 1]")).Text);
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
			back_link.Click ();
			return new EmployeeDataPage (driver);
		}
	}
}