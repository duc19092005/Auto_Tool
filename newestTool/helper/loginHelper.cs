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
    internal class loginHelper
    {
        public static void passwordServices(string password, UndetectedChromeDriver driver)
        {
            // Tạo biến đợi

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));

            // Kiểm tra trạng thái mật khẩu có đúng hay chưa

            bool passwordStatus = true;

            // Tìm kiếm ô input

            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));

            // Set Value ô input

            passwordInput.SendKeys(password);

            // Tìm kiếm nút "Đăng nhập"

            var loginFinalButton = wait.Until
                (ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));
            loginFinalButton.Click();

            // Tìm kiếm ô báo lỗi nếu gặp lỗi thì set trạng thái là false

            var getUserPasswordErrorBox =
                  driver.FindElements(By.CssSelector(".error.ext-error"));

            // Nếu có
            if (getUserPasswordErrorBox.Any())
            {
                passwordStatus = false;
                while (!passwordStatus)
                {
                    try
                    {
                        // Chỉnh màu thành đỏ

                        Console.ForegroundColor = ConsoleColor.Red;

                        
                        Console.WriteLine("Nhập sai Password Vui Lòng Nhập Lại");
                        Console.Write("Password : ");
                        Console.ResetColor();

                        // Nhập Lại

                        string newPassword = Console.ReadLine();

                        Console.ResetColor();

                        // Tìm kiếm lại ô input để set Data

                        passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));

                        // Tìm kiếm lại Button để ấn

                        loginFinalButton = wait.Until
                             (ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']")));

                        // Clear Data cũ trước khi chèn data mới

                        passwordInput.Clear();

                        // Chèn data mới

                        passwordInput.SendKeys(newPassword);

                        // Ấn vào nút "Đăng nhập"

                        loginFinalButton.Click();

                        // Tìm kiếm nếu có lỗi thì bắt tiếp

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
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Đã có lỗi xảy ra chi tiết lỗi : " + e.Message);
                    }
                }
            }
        }

        // Tương tự như PasswordServices

        public static void userServices(string userName, UndetectedChromeDriver driver)
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhập sai UserName Vui Lòng Nhập Lại");
                        Console.Write("UserName : ");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lỗi :" + e.Message);
                    }
                }
            }
        }
    }
}
