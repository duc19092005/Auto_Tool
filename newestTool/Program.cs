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
using newestTool.services;
using OpenQA.Selenium.DevTools.V100.HeapProfiler;
using System.Runtime.CompilerServices;
using System.Net;
using Cookie = OpenQA.Selenium.Cookie;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int options;

            Console.WriteLine(" ----------------------------- Menu -----------------------------");
            Console.WriteLine(" | 1. Chưa Đăng Nhập (Require Nhập mật khẩu and tên tài khoản)   |");
            Console.WriteLine(" | 2. Đã đăng nhập (Đã Có Cookie)                                |");
            Console.WriteLine(" | 3. Test                                                       |");
            Console.WriteLine(" | 0. Exits                                                      |");
            Console.WriteLine(" ----------------------------------------------------------------- ");

            Console.Write("--- Nhập Options của bạn --- : ");
            options = int.Parse(Console.ReadLine());
            if (options == 1)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("=== Nhập tên đăng nhập : ");
                string userName = Console.ReadLine();
                Console.WriteLine();
                Console.Write("=== Nhập mật khẩu : ");
                string password = Console.ReadLine();
                Console.WriteLine();
                Console.ResetColor();

                var loginStatus = await loginService.login(userName, password);

                if (loginStatus != null)
                {
                    Console.WriteLine("Cookie Path" + loginStatus.Path);
                    Console.WriteLine("Cookie EXP" + loginStatus.Expiry);
                    Console.WriteLine("Cookie Name" + loginStatus.Name);

                    Console.Write("Nhập đường dẫn File của bạn :");
                    string filePath = Console.ReadLine();
                    await DKMHOptions(loginStatus, filePath);
                }
                else
                {
                    Console.WriteLine("Login Failure");
                }


            }else if(options == 2)
            {
                Console.Write("Nhập Cookie của bạn trên trình duyệt Web lưu ý lấy cookie có tên là ASP.NET_SessionId cookie : ");
                string cookie = Console.ReadLine();
                Console.Write("Nhập đường dẫn File của bạn :");
                string filePath = Console.ReadLine();

                var stringToCookie = convertToCookieHelper.convertToCookie(cookie);

                await DKMHOptions(stringToCookie, filePath);
            }
            else if (options == 3)
            {
                Console.Write("Nhập Cookie của bạn trên trình duyệt Web lưu ý lấy cookie có tên là asp.Net cookie : ");
                string cookie = Console.ReadLine();

                var stringToCookie = convertToCookieHelper.convertToCookie(cookie);

                await testClass.DKHPFuncition(stringToCookie);
            }
            else if (options == 0)
            {
                Console.ReadKey();
            }
        }
        static async Task DKMHOptions(Cookie cookie, string filePath)
        {
            DKMHService dkmhService = new DKMHService();
            var getStatus = await dkmhService.DKMHServiceFuncitional(cookie, filePath);

            if (getStatus.Item1)
            {
                foreach (var items in getStatus.successKey)
                {
                    Console.WriteLine("Danh sách các môn thành công" + items);
                }

                foreach (var items in getStatus.failureKey)
                {
                    Console.WriteLine("Danh sách các môn thất bại" + items);
                }
            }
            else
            {
                Console.WriteLine(getStatus.Item2);
            }
        }

        // Hiện tại hàm này đã chạy cho bạn nào muốn test

        static async Task testCase(string username, string password)
        {
            await testClass.login(username, password);
        }
    }
}