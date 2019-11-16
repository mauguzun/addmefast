using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeFast
{

    class Program
    {
        static string selectedAcc;

        static void Main(string[] args)
        {
            Manager manager = new Manager();
            AddMeFast ad = new AddMeFast();

            List<string> accs = new List<string>();
            accs = manager.GetList("data/acc.txt");

            Console.WriteLine("pls select");
            for (int i = 0; i < accs.Count; i++)
            {
                Console.WriteLine(i + " / " + accs[i]);
            }

            int result = 0;
            Int32.TryParse(Console.ReadLine(), out result);
            selectedAcc = accs[result];
            //Console.WriteLine(accs[0]);
            //selectedAcc = accs[0];

            Console.WriteLine("show y/n");
            string show = Console.ReadLine();
            var dr = new DriverInstance();
            dr.Account = selectedAcc;
            dr.InitDriver(show == "y");


            var pinAcc = Directory.GetFiles("pins/");
            dr.Driver.Url = "https://pinterest.com";
            for (int i = 0; i < pinAcc.Count(); i++)
            {
                Console.WriteLine(i + " / " + pinAcc[i]);
            }
            while (true)
            {
                Console.WriteLine("pls select");
                int pinAccCount = 0;
                Int32.TryParse(Console.ReadLine(), out pinAccCount);
                dr.MakeLogin(pinAcc[pinAccCount]);
                dr.Driver.Url = "http://pinterest.com";
                Console.WriteLine(dr.Driver.FindElementsByCssSelector("#HeaderContent").Count() != 0);
                if (dr.Driver.FindElementsByCssSelector("#HeaderContent").Count() != 0)
                {
                    break;
                }

            }





            if (File.Exists("data/" + selectedAcc + ".xml"))
            {
                dr.Driver.Url = ad.home;
                dr.MakeLogin("data/" + selectedAcc + ".xml");
            }
            //$$('#HeaderContent')
            //HeaderContent
            ad.Driver = dr.Driver;
            ad.Account = selectedAcc;


            var checkPins = dr.Driver.FindElementsByCssSelector(".points_count");
            if (checkPins.Count == 0)
            {
                dr.InitDriver(true);
                while (true)
                {
                    Console.WriteLine("logined y/n");
                    ad.MakeLogin();

                    if (Console.ReadLine() == "y")
                    {
                        dr.Save();
                        break;
                    }
                }
            }



            ad.Repin();
            //1 show acc
            //2 choose  acc
            //3 try login via cookie
            //4 if not do manual 
            Console.ReadLine();
        }

        private static void LoginPin()
        {

        }
    }
}
