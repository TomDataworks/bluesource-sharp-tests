using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class AddEmployeePage : BaseWebPage
	{
		[FindsBy]
		private IWebElement employee_username;
		[FindsBy]
		private IWebElement employee_first_name;
		[FindsBy]
		private IWebElement employee_last_name;
		[FindsBy]
		private IWebElement employee_title_id;
		[FindsBy]
		private IWebElement employee_role;
		[FindsBy]
		private IWebElement employee_manager_id;
		[FindsBy]
		private IWebElement employee_status;
		[FindsBy]
		private IWebElement employee_bridge_time;
		[FindsBy]
		private IWebElement employee_location;
		[FindsBy]
		private IWebElement employee_start_date;
		[FindsBy]
		private IWebElement employee_cell_phone;
		[FindsBy]
		private IWebElement employee_office_phone;
		[FindsBy]
		private IWebElement employee_email;
		[FindsBy]
		private IWebElement employee_im_name;
		[FindsBy]
		private IWebElement employee_im_client;
		[FindsBy]
		private IWebElement employee_department_id;

		public AddEmployeePage ( IWebDriver driver ) : base (driver) {}

		public EmployeesPage AddEmployee(string name, string first, string last) {
            SyncElement (By.Id("employee_username"));
			this.employee_username.Click ();
			this.employee_username.SendKeys (name);
			this.employee_first_name.SendKeys (first);
			this.employee_last_name.SendKeys (last);
			this.employee_username.Submit ();

			return new EmployeesPage (driver);
		}
	}
}