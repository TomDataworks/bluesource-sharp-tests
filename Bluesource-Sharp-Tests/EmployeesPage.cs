using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace BluesourceSharpTests
{
	public struct Employee {
		IWebElement employee_username;
		IWebElement employee_first_name;
		IWebElement employee_last_name;
		IWebElement employee_title_id;
		IWebElement employee_role;
		IWebElement employee_manager_id;
		IWebElement employee_status;
		IWebElement employee_bridge_time;
		IWebElement employee_location;
		IWebElement employee_start_date;
		IWebElement employee_cell_phone;
		IWebElement employee_office_phone;
		IWebElement employee_email;
		IWebElement employee_im_name;
		IWebElement employee_im_client;
		IWebElement employee_department_id;
	}

	public class EmployeesPage : BaseWebPage
	{
		[FindsBy(How = How.XPath, Using = "//button[contains(., \"Add\")]")]
		private IWebElement addEmployee;

		[FindsBy(How = How.CssSelector, Using = "input#search-bar")]
		private IWebElement search_bar;

		public EmployeesPage ( IWebDriver driver ) : base (driver) {}

		public AddEmployeePage GotoAddEmployee() {
			addEmployee.Click ();
			return new AddEmployeePage (driver);
		}

		public EmployeesPage EnterInSearch(string name) {
			SyncElement (By.CssSelector ("#resource-content .ng-binding"));
			search_bar.SendKeys (name);
			return new EmployeesPage (driver);
		}

		public IReadOnlyCollection<IWebElement> GetMatchingEmployees() {
			return driver.FindElements (By.CssSelector ("div#resource-content div table tbody tr.ng-scope a"));
		}

		public EmployeeDataPage SelectFirstMatchingEmployee() {
			foreach (IWebElement we in GetMatchingEmployees()) {
				we.Click (); break;
			}
			return new EmployeeDataPage(driver);
		}
	}
}