using OpenQA.Selenium.Remote;
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

       public void Repin()
        {
            while(true)
            {
                try
                {
                    Driver.Url = "http://addmefast.com/free_points/pinterest_repin";
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

                    Thread.Sleep(new TimeSpan(0, 0, 7));
                    var boards = Driver.FindElementsByCssSelector("div[data-test-id='boardWithoutSection']");
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
      

    }
}
