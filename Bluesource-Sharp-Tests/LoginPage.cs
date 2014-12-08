using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class LoginPage : BaseWebPage
	{
		[FindsBy(How = How.Id, Using="employee_username")]
		private IWebElement username; // How.NAME = userName

		[FindsBy(How = How.Id, Using="employee_password")]
		private IWebElement password; // How.NAME = password

		public LoginPage ( IWebDriver driver ) : base (driver) {}

		public EmployeesPage DoLogin(string username, string password)
		{
			this.username.SendKeys(username);
			this.password.SendKeys(password);
			this.username.Submit ();

			return new EmployeesPage(driver);
		}

		public bool HasLoginLink() {
			return SyncElement (By.LinkText ("Can't log in?"));
		}
	}
}