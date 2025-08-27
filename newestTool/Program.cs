using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using SeleniumExtras.WaitHelpers;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using newestTool.helper;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("=== Nhập tên đăng nhập : ");

            string userName = Console.ReadLine();

            Console.WriteLine();

            Console.Write("=== Nhập mật khẩu : ");

            string password = Console.ReadLine();

            var getLoginStatus = await Login(userName, password);

            if (getLoginStatus.Item1)
            {
                Console.WriteLine();

                Console.WriteLine(getLoginStatus.Item2);

                Console.ResetColor();

                Console.ReadKey();

            }
        }

        static async Task<(bool, string)> Login(string userName, string password)
        {
            while (String.IsNullOrEmpty(userName))
            {
                Console.ForegroundColor= ConsoleColor.Red;

                Console.WriteLine("UserName bị trống");

                Console.WriteLine();

                Console.Write("Nhập lại UserName : ");

                userName = Console.ReadLine();
            }

            while (String.IsNullOrEmpty(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lỗi : Mật khẩu bị trống");
                Console.WriteLine();
                Console.Write("Nhập lại Password : ");
                password = Console.ReadLine();
            }

            using (var driver = UndetectedChromeDriver.Create(driverExecutablePath: await new ChromeDriverInstaller().Auto()))
            {
                // Mặc định là 5 tiếng Chờ WebLoad
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));
                try
                {
                    driver.GoToUrl("https://portal.huflit.edu.vn/");

                    var loginButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Đăng nhập")));
                    loginButton.Click();

                    var loginByMicrosoftButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[value='Office 365 for Student']")));
                    loginByMicrosoftButton.Click();


                    loginHelper.userServices(userName, driver);

                    loginHelper.passwordServices(password, driver);

                    var yesButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector
                        (".win-button.button_primary.high-contrast-overrides.button.ext-button.primary.ext-primary")));
                    yesButton.Click();

                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".navbar-header")));

                    var getCookie = driver.Manage().Cookies.GetCookieNamed("ASP.NET_SessionId");

                    // Lấy Data ra
                    var fileStatus = await readfileHelper.readFile("yourFilePath.txt");
                    
                    var getFileData = fileStatus.Item2; // Trích xuất từ dictionary ra để lấy data

                    var Tasks = new List<Task>();

                    foreach (var data in getFileData)
                    {
                        // Split data

                        string[] splitData = [];

                        if (data.Value.Contains(","))
                        {
                            splitData = data.Value.Split(",");
                        }
                        else
                        {
                            splitData = data.Value.Split();
                        }
                        // Cho nhiều Task chạy cùng 1 lúc 
                        Task task = dangKyHocPhanHelper.DKHPFuncition(getCookie, data.Key, splitData);
                        // Lưu Task vô List
                        Tasks.Add(task);
                    }

                    // Đợi tất cả Task Hoàn Thành

                    await Task.WhenAll(Tasks);
                    
                    return (true, "---------------- Đăng nhập Thành công ---------------------");
                }
                catch (Exception ex)
                {
                    return (false, $"Lỗi {ex.Message}");
                }
            }
        }
    }
}