
using System;

namespace ANTLR_HomeTask1
{
    public class ConsoleReadWrite: IConsoleReadWrite
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
