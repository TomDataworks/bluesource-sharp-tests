using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace BluesourceSharpTests
{
	[TestFixture ()]
	public class BluesourceTest
	{
		private IWebDriver driver;

		[TestFixtureSetUp] public void Init()
		{
			driver = new FirefoxDriver();
			driver.Navigate ().GoToUrl ("http://bluesourcestaging.herokuapp.com/login");
			driver.Manage ().Timeouts ().ImplicitlyWait (TimeSpan.FromSeconds (10));
		}

		[TestFixtureTearDown] public void Cleanup()
		{
			driver.Quit ();
		}

		/*[Test ()]
		public void TestLoginLogout()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			Assert.IsTrue (nav.HasLogoutLink ());
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddEmployee()
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			Assert.IsTrue (nav.HasLogoutLink ());
			AddEmployeePage newempl = empl.GotoAddEmployee ();
			string user = Guid.NewGuid ().ToString ();
			string first = Guid.NewGuid().ToString ();
			string last = Guid.NewGuid ().ToString ();
			empl = newempl.AddEmployee (user, first, last);
			Assert.IsTrue (nav.HasAddedEmployeeText ());
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddDepartment()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			DepartmentsPage depts = nav.GotoDepartments ();
			AddDepartmentPage addDept = depts.GotoAddDepartment ();
			depts = addDept.AddDepartment ("The Investigators");
			Assert.IsTrue (nav.HasAddedDepartmentText ());
			Assert.IsNotNull (depts.FindDepartmentByName ("The Investigators"));
			depts = depts.TrashDepartment ("The Investigators");
			// Assert.IsNull (depts.FindDepartmentByName ("The Investigators"));
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddTitle()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			TitlesPage titles = nav.GotoTitles ();
			AddTitlePage addTitle = titles.GotoAddTitle();
			titles = addTitle.AddTitle ("Agent");
			Assert.IsTrue (nav.HasAddedTitleText ());
			Assert.IsNotNull (titles.FindTitleByName ("Agent"));
			titles = titles.TrashTitle ("Agent");
			// Assert.IsNull (titles.FindTitleByName ("Agent"));
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}*/

		[Test ()]
		public void TestTimeOff()
		{
			StreamReader re = new StreamReader("bluesource-tests.json");
			JsonTextReader reader = new JsonTextReader(re);
			JsonSerializer se = new JsonSerializer();
			object parsedData = se.Deserialize(reader);

			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			empl.EnterInSearch ("Kazirick Revele");
			EmployeeDataPage data = empl.SelectFirstMatchingEmployee ();
			ManageTimeOffPage timeOff = data.GotoManageTimeOff ();
			timeOff = timeOff.SetVacationInfo (
				DateTime.ParseExact ("29122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("30122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			);
			IWebElement time = timeOff.GetVacationInfo (
				DateTime.ParseExact ("29122014", "ddMMyyyy", CultureInfo.InvariantCulture)
			);
			Assert.IsNotNull (time);
			Assert.IsTrue (time.FindElement (By.CssSelector (".business-days")).FindElement(By.XPath("strong")).Text.Equals("2"));
			timeOff.TrashVacationInfo (
				DateTime.ParseExact ("29122014", "ddMMyyyy", CultureInfo.InvariantCulture)
			);
		}
	}
}

