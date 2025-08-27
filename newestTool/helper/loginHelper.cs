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
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhập sai Password Vui Lòng Nhập Lại");
                        Console.Write("Password : ");
                        Console.ResetColor();
                        string newPassword = Console.ReadLine();
                        Console.ResetColor();
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
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Đã có lỗi xảy ra chi tiết lỗi : " + e.Message);
                    }
                }
            }
        }

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
