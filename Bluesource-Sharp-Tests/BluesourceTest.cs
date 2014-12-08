using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects; // *
using NUnit.Framework;
using System;
using System.Globalization;

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
		public void TestTimeOffTwoDays()
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			empl.EnterInSearch ("Kazirick Revele");
			EmployeeDataPage data = empl.SelectFirstMatchingEmployee ();
			ManageTimeOffPage timeOff = data.GotoManageTimeOff ();
			timeOff = timeOff.SetVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("29122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			);
			Assert.IsNotNull (timeOff.GetVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture)
			));
			/*timeOff.RemoveVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("29122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			);*/
			foreach (IWebElement we in timeOff.GetAllTimeOff()) {
				Console.WriteLine (we.Text);
			}
		}

		/*[Test ()]
		public void TestTimeOffSingleDay()
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			empl.EnterInSearch ("Kazirick Revele");
			EmployeeDataPage data = empl.SelectFirstMatchingEmployee ();
			ManageTimeOffPage timeOff = data.GotoManageTimeOff ();
			timeOff = timeOff.SetVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("28122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			);
			Assert.IsNotNull (timeOff.GetVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("28122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			));
			foreach (IWebElement we in timeOff.GetAllTimeOff()) {
				Console.WriteLine (we.Text);
			}
		}*/
	}
}

