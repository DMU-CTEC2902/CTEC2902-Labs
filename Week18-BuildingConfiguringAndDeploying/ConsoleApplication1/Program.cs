using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.WriteLine("Program has finished. Press any key to quit.");

            Console.ReadLine();

        }
    }
}
