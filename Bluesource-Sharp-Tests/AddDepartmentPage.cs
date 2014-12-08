using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class AddDepartmentPage : BaseWebPage
	{
		[FindsBy]
		private IWebElement department_name;
		[FindsBy]
		private IWebElement department_department_id;

		public AddDepartmentPage ( IWebDriver driver ) : base (driver) {}

		public DepartmentsPage AddDepartment(string name) {
			if (SyncElement (By.Id("department_name"))) {
				department_name.SendKeys (name);
				department_department_id.FindElement (By.XPath ("//option[@value != '']")).Click ();
				department_name.Submit ();
			}
			return new DepartmentsPage (driver);
		}
	}
}