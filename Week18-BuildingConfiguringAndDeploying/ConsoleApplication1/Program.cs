using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Console = Colorful.Console;
using System.Configuration;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine("Write something to the output window...");

            bool inAGoodMood = Properties.Settings.Default.InAGoodMood;

            string greeting = string.Empty;

            if (inAGoodMood)
            {
                greeting = Properties.Settings.Default.GreetingMessage;

            }
            else
            {
                greeting = Properties.Settings.Default.AngryMessage;
            }
                
            Console.WriteLine(greeting);

            string tiredMessage = string.Empty;

            try
            {
                tiredMessage = ConfigurationManager.AppSettings["tiredMessage"];
            }
            catch(Exception ex)
            {
                tiredMessage = ex.Message;

            }

            Console.WriteLine(tiredMessage);

            Console.WriteLine("Program has finished. Press any key to quit.", Color.Aquamarine);

            Console.ReadLine();

        }
    }
}
