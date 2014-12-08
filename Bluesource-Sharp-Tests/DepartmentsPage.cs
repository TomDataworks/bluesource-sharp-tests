using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class DepartmentsPage : BaseWebPage
	{
		[FindsBy(How = How.LinkText, Using = "Add Department")]
		private IWebElement addDepartment;

		public DepartmentsPage ( IWebDriver driver ) : base (driver) {}

		public AddDepartmentPage GotoAddDepartment() {
			addDepartment.Click ();
			return new AddDepartmentPage (driver);
		}

		public IWebElement FindDepartmentByName( string name ) {
			return driver.FindElement (By.XPath ("//li[contains(., '" + name + "')]"));
		}

		public DepartmentsPage TrashDepartment( string name ) {
			FindDepartmentByName(name).FindElement(By.CssSelector("span.glyphicon.glyphicon-trash")).Click();
			driver.SwitchTo ().Alert ().Accept ();
			return new DepartmentsPage (driver);
		}
	}
}