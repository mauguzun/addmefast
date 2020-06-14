using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddMeFast
{
    public class AddMeFast
    {
        public string home = "http://addmefast.com/";
       
       public  String Account { get; set; }
       public  RemoteWebDriver Driver { get; set; }

        public AddMeFast()
        {

        }
        public void MakeLogin()
        {
            Driver.Url = this.home;
            Driver.Url = this.home+"/login";
            try

            {
                string [] values = this.Account.Split('_');

                var xx = Driver.FindElementByName("email");
                Driver.FindElementByCssSelector(".first [name='email']").SendKeys(values[0]);
                Driver.FindElementByCssSelector(".first [name='remember']").Click();
                Driver.FindElementByCssSelector(" [name='password']").SendKeys(values[1]);
                Driver.FindElementByCssSelector(".last [name='login_button']").Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected void Bonuses()
        {
            Driver.Url = "http://addmefast.com/bonus_points";
            var data = Driver.FindElementsByCssSelector("#subscribeButton");

            if (data.Count() > 0)
            {
                Console.WriteLine(data[0].Text);
            }

            if (Driver.FindElementsById("subscribeButton").Count() != 0 )
            {
                Driver.FindElementById("subscribeButton").Click();
                Console.Title = " bonuses ";
            }
            



        }
       public void Repin()
        {
            while(true)
            {
                try
                {
                    // 
                    Bonuses();


                    Driver.Url = "https://addmefast.com/free_points/pinterest_save";

                    if(Driver.FindElementsByClassName("footer-add").Count == 0)
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 10));
                    }
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
               //     Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    var points = Driver.FindElementsByCssSelector(".points_count");
                    if (points != null)
                    {
                        Console.WriteLine(points[0].Text);

                    }
                    else
                    {
                        Console.WriteLine("points ?");
                    }


                    Thread.Sleep(new TimeSpan(0, 0, 5));

             
                  
                    Driver.FindElementByCssSelector("a.single_like_button").Click();

                    Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                    ((IJavaScriptExecutor)Driver).ExecuteScript("window.resizeTo(2000, 1000);");

                    Thread.Sleep(new TimeSpan(0, 0, 7));
                    var boards = Driver.FindElementsByCssSelector("div[data-test-id='boardWithoutSection']");
                    var save = Driver.FindElementsByCssSelector("[data-test-id='SaveButton']");
                    if (save != null && save.Count > 0  )
                    {
                    
                      
                        save[0].Click();
                        Thread.Sleep(new TimeSpan(0, 0, 4));
                        Console.WriteLine("pinned" + DateTime.Now.ToShortTimeString());
                        Thread.Sleep(new TimeSpan(0, 0, 4));
                        Driver.Close();
                        Thread.Sleep(new TimeSpan(0, 0, 2));
                    }
                   
                    if (boards.Count == 0)
                    {
                        Console.WriteLine("not boards");
                        // return;
                    }
                    foreach (var board in boards)
                    {
                        try
                        {
                            board.Click();
                            Thread.Sleep(new TimeSpan(0, 0, 4));
                            Console.WriteLine("pinned" + DateTime.Now.ToShortTimeString());
                            Driver.Close();
                            Thread.Sleep(new TimeSpan(0, 0, 2));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles.First());
                }
            }
          
        }
      

        public void AddAccount(string account)
        {
            try
            {
                Driver.Url = "http://addmefast.com/add_edit_sites";

                var select = Driver.FindElementById("l_type");
                var selectElement = new SelectElement(select);
                selectElement.SelectByText("Pinterest Followers");
                Driver.FindElementById("l_url").SendKeys(account);
         
                Driver.FindElementById("l_cpc").SendKeys(OpenQA.Selenium.Keys.Backspace);
                Driver.FindElementById("l_cpc").SendKeys(OpenQA.Selenium.Keys.Backspace);
                Driver.FindElementById("l_cpc").SendKeys(OpenQA.Selenium.Keys.Backspace);
                Driver.FindElementById("l_cpc").SendKeys("5");
                Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
       
                Driver.FindElementByCssSelector("input[type='submit']").Click();
                Thread.Sleep(new TimeSpan(0, 0, 5));

            }
            catch
            {

            }
          
          

        }
    }
}
