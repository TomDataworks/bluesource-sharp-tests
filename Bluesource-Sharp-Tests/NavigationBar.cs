using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class NavigationBar : BaseWebPage
	{
		[FindsBy(How = How.XPath, Using = "//a[@href=\"/logout\"]")]
		private IWebElement logout;

		[FindsBy(How = How.LinkText, Using = "Admin")]
		private IWebElement admin;

		[FindsBy(How = How.LinkText, Using = "Employees")]
		private IWebElement employees;

		[FindsBy(How = How.LinkText, Using = "Departments")]
		private IWebElement departments;

		[FindsBy(How = How.LinkText, Using = "Titles")]
		private IWebElement titles;

		public NavigationBar ( IWebDriver driver ) : base (driver) {}

		public bool HasLogoutLink() {
			return SyncElement (By.XPath("//a[@href=\"/logout\"]"));
		}

		public bool HasAddedEmployeeText() {
			return SyncElement (By.XPath ("//div[contains(., \"Employee successfully created.\")]"));
		}

		public bool HasAddedDepartmentText() {
			return SyncElement (By.XPath ("//div[contains(., \"Department successfully created.\")]"));
		}

		public bool HasAddedTitleText() {
			return SyncElement (By.XPath ("//div[contains(., \"Title successfully created.\")]"));
		}
			
		public LoginPage DoLogout() {
			logout.Click ();
			return new LoginPage (driver);
		}

		public EmployeesPage GotoEmployees() {
			employees.Click (); 
			return new EmployeesPage (driver);
		}

		public DepartmentsPage GotoDepartments() {
			admin.Click ();
			departments.Click ();
			return new DepartmentsPage (driver);
		}

		public TitlesPage GotoTitles() {
			admin.Click ();
			titles.Click ();
			return new TitlesPage (driver);
		}
	}
}