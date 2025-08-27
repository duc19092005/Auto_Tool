using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using SeleniumExtras.WaitHelpers;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Nhập tên đăng nhập : ");
            string userName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Nhập mật khẩu : ");
            string password = Console.ReadLine();

            var getLoginStatus = await Login(userName, password);
            if (getLoginStatus.Item1)
            {
                Console.WriteLine();
                Console.WriteLine(getLoginStatus.Item2);
            }
        }

        static async Task<(bool, string)> Login(string userName, string password)
        {


            while (String.IsNullOrEmpty(userName))
            {
                Console.WriteLine("UserName bị trống");
                Console.WriteLine();
                Console.Write("Nhập lại UserName : ");
                userName = Console.ReadLine();
            }

            while (String.IsNullOrEmpty(password))
            {
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


                    userServices(userName, driver);

                    passwordServices(password, driver);

                    var yesButton =
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector
                        (".win-button.button_primary.high-contrast-overrides.button.ext-button.primary.ext-primary")));
                    yesButton.Click();

                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".navbar-header")));
                    Console.ReadKey();
                    return (true, "Đăng nhập Thành công");
                }
                catch (Exception ex)
                {
                    return (false, $"Lỗi {ex.Message}");
                }
            }
        }

        static void passwordServices(string password, UndetectedChromeDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));
            bool passwordStatus = true;
            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));
            passwordInput.SendKeys(password);

            var loginFinalButton = wait.Until
                (ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));
            loginFinalButton.Click();

            var getUserPasswordErrorBox =
                  driver.FindElements(By.CssSelector(".error.ext-error"));

            if (getUserPasswordErrorBox.Any())
            {
                passwordStatus = false;
                while (!passwordStatus)
                {
                    Console.WriteLine("Nhập sai Password Vui Lòng Nhập Lại");
                    Console.Write("Password : ");
                    string newPassword = Console.ReadLine();
                    passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));
                    loginFinalButton = wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));
                    passwordInput.Clear();
                    passwordInput.SendKeys(newPassword);
                    loginFinalButton.Click();
                    var getUserPasswordErrorBoxRenew =
                            driver.FindElements(By.CssSelector(".error.ext-error"));
                    if (getUserPasswordErrorBoxRenew.Any())
                    {
                        passwordStatus = false;
                    }
                    else
                    {
                        passwordStatus = true;
                    }
                }
            }
        }

        static void userServices(string userName, UndetectedChromeDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));
            bool userNameStatus = true;

            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='email']")));
            emailInput.SendKeys(userName);

            var nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));
            nextButton.Click();

            var getUserNameErrorBox =
                  driver.FindElements(By.CssSelector(".col-md-24.error.ext-error"));

            if (getUserNameErrorBox.Any())
            {
                userNameStatus = false;
                while (!userNameStatus)
                {
                    try
                    {
                        Console.WriteLine("Nhập sai UserName Vui Lòng Nhập Lại");
                        Console.Write("UserName : ");
                        string newUserName = Console.ReadLine();
                        emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='email']")));
                        nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));
                        emailInput.Clear();
                        emailInput.SendKeys(newUserName);
                        nextButton.Click();
                        var getUserNameErrorBoxRenew =
                                driver.FindElements(By.CssSelector(".col-md-24.error.ext-error"));
                        if (getUserNameErrorBoxRenew.Any())
                        {
                            userNameStatus = false;
                        }
                        else
                        {
                            userNameStatus = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Lỗi :" + e.Message);
                    }
                }
            }
        }
    }
}