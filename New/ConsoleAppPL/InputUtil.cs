using System;

namespace ConsoleAppPL
{
    class InputUtil
    {
        public static bool isINT(String num)
        {
            int n;
            return int.TryParse(num, out n);
        }

        public static int readINT()
        {
            start:
            string num = Console.ReadLine();
            
            if(isINT(num))
            {
                return int.Parse(num);
            }
            else
            {
                Console.WriteLine("\t");
                Console.WriteLine("Enter the wrong format! Please re-enter: ");
                goto start;
            }
        }
    }
}