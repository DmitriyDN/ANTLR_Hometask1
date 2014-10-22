
using System;
using System.Collections.Generic;
using Calculator;

namespace ANTLR_HomeTask1
{
    public class Program
    {
        private static IDataExchange _dataExchange = new DataExchange(new ConsoleReadWrite());

        static void Main()
        {
            Console.WriteLine("Enter string to calculate and press enter:");

            var stream = _dataExchange.ReadData();

            var lexer = new Lexer();
            List<string> dividedString = lexer.DivStringFroTokens(stream);

            var parser = new Parser();
            var result = parser.Calculate(dividedString);

            Console.WriteLine("result = {0}", result);
            Console.ReadLine();
        }
    }
}
