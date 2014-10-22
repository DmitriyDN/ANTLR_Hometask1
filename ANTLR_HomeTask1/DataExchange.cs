
using System;

namespace ANTLR_HomeTask1
{
    public class DataExchange : IDataExchange
    {
        private IConsoleReadWrite _consoleDataExchange;

        public DataExchange(IConsoleReadWrite consoleReadWrite)
        {
            _consoleDataExchange = consoleReadWrite;
        }

        public string ReadData()
        {
            return _consoleDataExchange.ReadLine();
        }
    }
}
