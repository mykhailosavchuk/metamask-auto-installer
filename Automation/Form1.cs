using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using OpenQA.Selenium.Chrome; //module for control web browser.
using OpenQA.Selenium.Support.UI; //module for control web browser.
using OpenQA.Selenium; //module for control web browser.
using WebDriverManager.DriverConfigs.Impl; //module for web browser.

using HtmlAgilityPack;

namespace Automation
{
    public partial class Form1 : Form
    {
        ChromeDriver driver;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // start automation.
            Task task = Task.Run((System.Action)AutomationTask);
        }

        public void AutomationTask()
        {
            // switch to current activated tab from browser.
            String parentWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(parentWindowHandle);

            // go to "google.com".
            driver.Url = "https://www.google.com/";
            // input "C# tutorial" for search.
            driver.FindElement(By.XPath("//input[@name='q']")).SendKeys("C# tutorial");
            // I did ready 1 second for you need to see the screen.  You can remove this code.
            Thread.Sleep(1000);
            // start search.
            driver.FindElement(By.XPath("//input[@name='btnK']")).Click();
            // I did ready 1 second for you need to see the screen.  You can remove this code.
            Thread.Sleep(1000);
            // click the C# tutorial link.
            driver.FindElement(By.XPath("//div[@id='search']/div/div/div[2]/div/div/div[1]/a")).Click();
            // get page source from web browser.
            string htmlCode = driver.PageSource;
            if (txtSource.InvokeRequired)
            {
                txtSource.Invoke(new System.Action(() => txtSource.Text = htmlCode));
            }
            else
            {
                txtSource.Text = htmlCode;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // open the chrome browser.
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments(@"--user-data-dir=C:\Users\Develop\AppData\Local\Google\Chrome\User Data\Profile 1");
            options.AddExtensions("MetaMask.crx");
            //options.AddAdditionalCapability("browserstack.safari.allowAllCookies", true);
            driver = new ChromeDriver(options);
            
            String parentWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(parentWindowHandle);
            driver.Url = "https://www.google.com/";
            //driver.Manage().Cookies.DeleteAllCookies();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // switch to current activated tab from browser.
            String parentWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(parentWindowHandle);

            // go to "google.com".
            driver.Url = "chrome-extension://nkbihfbeogaeaoehlefnkodbefgpgknn/home.html#initialize/create-password/import-with-seed-phrase";
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("escape call robust wolf bleak treat measure multiply gesture unfold tilt gravity");
            driver.FindElement(By.XPath("//input[@id='password']")).SendKeys("test1234");
            driver.FindElement(By.XPath("//input[@id='confirm-password']")).SendKeys("test1234");
            driver.FindElement(By.XPath("//div[@class='first-time-flow__checkbox first-time-flow__terms']")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@role='button']")).Click();
        }
    }
}
