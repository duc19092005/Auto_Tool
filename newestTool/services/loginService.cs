using newestTool.helper;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newestTool.services
{
    internal class loginService
    {
        public static async Task<Cookie> login(string userName, string password)
        {
            while (String.IsNullOrEmpty(userName))
            {
                // Chỉnh màu thành màu đỏ nếu gặp lỗi

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("UserName bị trống");

                Console.WriteLine();

                Console.Write("Nhập lại UserName : ");

                // Yêu cầu nhập lại Username nếu gặp lỗi sai username

                userName = Console.ReadLine();
            }

            while (String.IsNullOrEmpty(password))
            {
                // Chỉnh màu thành màu đỏ nếu gặp lỗi

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Lỗi : Mật khẩu bị trống");

                Console.WriteLine();

                Console.Write("Nhập lại Password : ");

                // Yêu cầu nhập lại Password nếu gặp lỗi sai Password

                password = Console.ReadLine();
            }

            // Tạo Driver mới cho trình duyệt Chrome

            using (var driver = UndetectedChromeDriver.Create(driverExecutablePath: await new ChromeDriverInstaller().Auto()))
            {
                // Mặc định là 5 tiếng Chờ WebLoad

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));
                try
                {
                    // Trước Tiên Trỏ tới Trang Huflit để đăng nhập

                    driver.GoToUrl("https://portal.huflit.edu.vn/");

                    // Tìm kiếm nút đăng nhập ở trang Portal Huflit

                    var loginButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Đăng nhập")));
                    loginButton.Click();

                    // Tìm kiếm đăng nhập cho sinh viên

                    var loginByMicrosoftButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[value='Office 365 for Student']")));
                    loginByMicrosoftButton.Click();

                    // Gọi Helper Services

                    loginHelper.userServices(userName, driver);

                    // Gọi Helper Services

                    loginHelper.passwordServices(password, driver);

                    // Tìm kiếm nút đồng ý tiếp tục

                    var yesButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector
                        (".win-button.button_primary.high-contrast-overrides.button.ext-button.primary.ext-primary")));
                    yesButton.Click();

                    // Kiểm tra xem hiện tại có đang ở trang chủ chính hay chưa thông qua navbar-header

                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".navbar-header")));

                    // Lấy Cookie để chia sẻ cho các trang khác

                    var getCookie = driver.Manage().Cookies.GetCookieNamed("ASP.NET_SessionId");

                    Console.WriteLine("-------------- Đã Đăng Nhập Thành Công -----------------");

                    return getCookie;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi {ex.Message}");

                    return null!;
                }
            }
        }
    }
}
