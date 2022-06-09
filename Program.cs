using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendIt
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string input, output, pattern,
                replacement, time, quit;
            bool repeat = true;

            Console.WriteLine("SendIt! (as Keystrokes) -- v1.0\n" +
                "  .DIRECTIONS\n" +
                "  1. Copy text to your clipboard.\n" +
                "  2. Paste into this console**.\n" +
                "  3. Hit [ENTER] on your keyboard.\n" +
                "  4. In the following 5 seconds after you hit [ENTER], focus (click) your mouse in the window where you intend to send your text.\n\n" +
                "  Your text will then be sent to wherever your mouse was focused by entering as if it was being typed out.\n" +
                "  **For security, the console AND your clipboard are cleared right before each concurrent send as well as on program exit.\n");

            while (repeat == true)
            {
                Console.WriteLine("Paste below and hit [ENTER]");
                input = Console.ReadLine();
                pattern = "[(\\{+^%~\\})]";
                replacement = "{$0}";
                output = Regex.Replace(input, pattern, replacement);
                Clipboard.Clear();

                for (int i = 5; i > 0; i--)
                {
                    Console.Write(new string('.', (i-1)));
                    Console.WriteLine(i);
                    Task.Delay(1000).Wait();
                }

                time = DateTime.Now.ToString("HH:mm:ss.ffff");

                try
                {
                    SendKeys.SendWait(output);
                    Console.WriteLine($">> {input.Length} characters sent at {time}");
                    Console.WriteLine("Press [ENTER] to go again or Q to quit...");
                    quit = Console.ReadLine();
                    Console.Clear();
                    if (quit.ToLower() == "q")
                    {
                        repeat = false;
                        break;
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine($"{ex} exception caught.");
                    Console.Clear();
                }
            }
        }
    }
}
