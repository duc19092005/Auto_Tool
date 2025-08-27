using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumUndetectedChromeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace newestTool.helper
{
    internal class dangKyHocPhanHelper
    {
        public async static Task DKHPFuncition(Cookie cookie , string key , string[]value)
        {
            try
            {
                // Phiên làm việc đợi khá lâu do có thể có nhiều người đặt

                var driver = UndetectedChromeDriver.Create(driverExecutablePath: await new ChromeDriverInstaller().Auto());
                // Trỏ tới trang đk học phần 
                driver.GoToUrl("https://portal.huflit.edu.vn/Home/DangKyHocPhan");
                driver.Manage().Cookies.AddCookie(cookie);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));

                // Trước tiên tìm kiếm môn trước 

                // Sử dụng cơ chế bất đồng bộ để chạy 5 task cùng 1 lúc

                var searchMaMon = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("CssValue")));

                // Tiếp tục tìm tiếp mã học phần

                // Trước tiên lấy 1 cái ra để check xem đã hiển thị hay chưa

                var checkIfExist = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("Tùy thuộc vào data có gì" + value.Select(x => x.ToString()).First())));

                // Nếu đã tồn tại rồi thì tiếp tục đăng ký môn

                foreach(var items in value){
                    var findCheckBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("Tùy thuộc vào data có gì" + items)));
                    if (findCheckBox.Selected)
                    {
                        // Bỏ qua
                    }else
                    {
                        findCheckBox.Click();
                    }    
                }

                // Sau khi tất cả đã được click thì tiến hành ấn nút đăng ký học phần

                var findSignButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("Tùy thuộc vào data có gì" + "Đây là Button")));

                findSignButton.Click();

            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi :" + e.Message);
            }
        }
    }
}
