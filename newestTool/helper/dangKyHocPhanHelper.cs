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
        public async static Task<(bool , string , string)> DKHPFuncition(Cookie cookie , string key , string[]value)
        {
            try
            {

                var driver = UndetectedChromeDriver.Create(driverExecutablePath: await new ChromeDriverInstaller().Auto());

                // Trỏ tới trang đk học phần 

                driver.GoToUrl("https://portal.huflit.edu.vn/Home/DangKyHocPhan");

                // Thêm Cookie vào Header

                driver.Manage().Cookies.AddCookie(cookie);

                // Đợi khá lâu do nếu web bị nghẽn thì còn cơ hội vào đc

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18000));

                // Trước tiên tìm kiếm môn trước 

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

                return (true, key , "Đăng ký học phần thành công");

            }
            catch (Exception e)
            {
                return (false, key , "Lỗi :" + e.Message);
            }
        }
    }
}
