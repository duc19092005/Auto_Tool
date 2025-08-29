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
using System.Globalization;

namespace newestTool.services
{
    internal class DKMHService
    {
        public async Task<(bool , string , List<string> successKey , List<string> failureKey)> DKMHServiceFuncitional(Cookie cookie , string filePath)
        {
            try
            {
                // Get FilePath 
                if (cookie == null)
                {
                    return (false, "Lỗi Cookie Bị Null" , [] , []);
                }

                var getFileData = await fileReaderHelper.readFile(filePath);

                if (getFileData.Item2 != null && getFileData.Item2.Any())
                {
                    var Tasks = new List<Task<(bool , string , string)>>();

                    // Duyệt qua Dictionary để lấy key và value

                    foreach (var data in getFileData.Item2)
                    {
                        // Split data ra để cho vào paramter

                        string[] splitData = [];

                        // nếu dính vào Options 2

                        if (data.Value.Contains(","))
                        {
                            splitData = data.Value.Split(",");
                        }
                        else // Options 1
                        {
                            splitData = data.Value.Split();
                        }

                        // Tạo Task mới

                        var task = dangKyHocPhanHelper.DKHPFuncition(cookie, data.Key, splitData);

                        // Thêm Task mới vào List

                        Tasks.Add(task);
                    }

                    // Chờ cho đến khi cả 5 Task Chạy Thành Công

                    var results = await Task.WhenAll(Tasks);

                    // Thông báo đăng ký môn thành công

                    List<string> successKey = new List<string>();
                    List<string> failureKey = new List<string>();

                    foreach (var result in results) 
                    { 
                        if (result.Item1)
                        {
                            successKey.Add(result.Item2);
                            Console.WriteLine($"✅ Đăng ký học phần {result.Item2} thành công.");
                        }else if (!result.Item1)
                        {
                            failureKey.Add(result.Item2);
                            Console.WriteLine($"✅ Đăng ký học phần {result.Item2} thất bại. + Lỗi : {result.Item3}");
                        }
                    }

                    return (true, "Thành công" , successKey , failureKey);
                }
                return (false, "Lỗi File bị trống", [], []);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi chi tiết lỗi là : {ex.Message}" , [] , []);
            }
        }
    }
}
