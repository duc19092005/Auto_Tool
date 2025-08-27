using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newestTool.helper
{
    internal class testClass
    {
        public async static Task DKHPFuncition(Cookie cookie)
        {
            try
            {
                // Phiên làm việc đợi khá lâu do có thể có nhiều người đặt

                var driver = UndetectedChromeDriver.Create(driverExecutablePath: await new ChromeDriverInstaller().Auto());
                driver.GoToUrl("https://portal.huflit.edu.vn/");
                driver.Manage().Cookies.AddCookie(cookie);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));

                driver.Navigate().GoToUrl("https://portal.huflit.edu.vn/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi :" + e.Message);
            }
        }
    }
}
