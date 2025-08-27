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
            Console.WriteLine();
            Console.ResetColor();

            int options;

            Console.WriteLine(" -------------- Menu ------------------- ");
            Console.WriteLine(" | 1. Test Case                        |");
            Console.WriteLine(" | 2. Real Case                        |");
            Console.WriteLine(" | 0. Exits                            |");
            Console.WriteLine(" --------------------------------------- ");

            Console.Write("--- Nhập Options của bạn --- : ");
            options = int.Parse(Console.ReadLine());
            if (options == 1)
            {
                await testCase(userName, password);
            }else if(options == 2)
            {
                await DKMH(userName, password, "YourFilePath");
            }else if (options == 0)
            {
                Console.ReadKey();
            }
        }

        // Hiện tại hàm này chưa chạy do mình chưa vào được trang DKHP

        static async Task DKMH(string username , string password , string filePath)
        {
            loginServices loginServices = new loginServices();
            await loginServices.login(username, password , filePath);
        }

        // Hiện tại hàm này đã chạy cho bạn nào muốn test

        static async Task testCase(string username, string password)
        {
            await testClass.login(username, password);
        }
    }
}