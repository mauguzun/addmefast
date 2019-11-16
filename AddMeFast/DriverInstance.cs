using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PinCombain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AddMeFast
{
    class DriverInstance
    {
        public RemoteWebDriver Driver { get; set; }
        public string Account { get; set; }
        public void InitDriver(bool visible = false)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();

            if (!visible)
                options.AddArguments("headless");

            options.AddArgument("--window-size=1920,4080");

            Driver = new ChromeDriver(driverService, options);

        }
        public void Save()
        {
            var xs = Driver.Manage().Cookies.GetCookieNamed("_auth");
            var cookies = Driver.Manage().Cookies.AllCookies;

            List<DCookie> listDc = new List<DCookie>();
            foreach (OpenQA.Selenium.Cookie cookie in cookies)
            {
                //_auth=1
                var dCookie = new DCookie();
                dCookie.Domain = cookie.Domain;
                dCookie.Expiry = cookie.Expiry;
                dCookie.Name = cookie.Name;
                dCookie.Path = cookie.Path;
                dCookie.Value = cookie.Value;
                dCookie.Secure = cookie.Secure;

                listDc.Add(dCookie);
            }
            XmlSerializer ser = new XmlSerializer(typeof(List<DCookie>), new XmlRootAttribute("list"));

            using (FileStream fs = new FileStream("data/" + Account + ".xml", FileMode.Create))
            {
                ser.Serialize(fs, listDc);
            }
        }






        public void MakeLogin(string filePath)
        {

            List<DCookie> dCookie;
            using (var reader = new StreamReader(filePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<DCookie>),
                    new XmlRootAttribute("list"));
                dCookie = (List<DCookie>)deserializer.Deserialize(reader);
            }

            foreach (var cookie in dCookie)
            {
                Driver.Manage().Cookies.AddCookie(cookie.GetCookie());
            }


        }

    }
}
