
using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Lexer: ILexer
    {
        //private List<int> _numbers = new List<int>();
        //private List<string> _operations = new List<string>();  
        //private List<string> _dividedString = new List<string>(); 

        public List<string> DivStringFroTokens(string input)
        {
            List<string> dividedString = new List<string>(); 
            bool isNumber = false;
            string number = "";

            foreach (var symbol in input)
            {
                if (symbol >= 48 && symbol <= 57) // If it is number
                {
                    if (!isNumber)
                    {
                        isNumber = true;
                    }
                    number += symbol;
                }
                else if (symbol >= 40 && symbol <= 43 || symbol == 45 || symbol == 47) // ()*+ - /
                {
                    if (isNumber)
                    {
                        //_numbers.Add(Convert.ToInt32(number));
                        dividedString.Add(number);
                        number = "";
                        isNumber = false;
                    }
                    //_operations.Add(symbol.ToString());
                    dividedString.Add(symbol.ToString());
                }
            }
            if (number.Length > 0)
            {
                //_numbers.Add(Convert.ToInt32(number));
                dividedString.Add(number);
            }
            //listOfNumbers = _numbers;
            //listOfOperations = _operations;

            return dividedString;
        }
    }
}
