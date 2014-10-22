
using System.Collections.Generic;

namespace Calculator
{
    public interface ILexer
    {
        List<string> DivStringFroTokens(string input);
    }
}
