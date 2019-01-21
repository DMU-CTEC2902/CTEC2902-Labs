using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using System.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Debug.WriteLine("Write something to the output window...");
            Console.WriteLine("Program has finished. Press enter to quit.", Color.Aquamarine);

            //1) Applicaton Settings Configuration
            string greeting = Properties.Settings.Default.GreetingMessage;
            Console.WriteLine(greeting);

            //2) Application Settings 
            string tiredMessage = ConfigurationManager.AppSettings["tiredMessage"];
            Console.WriteLine(tiredMessage);


            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
