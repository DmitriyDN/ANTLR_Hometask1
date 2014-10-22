
namespace Calculator
{
    public class Calculation: ICalculation
    {
        public int Add(int left, int right)
        {
            return left + right;
        }

        public int Sub(int left, int right)
        {
            return left - right;
        }

        public int Mul(int left, int right)
        {
            return left * right;
        }

        public int Div(int left, int right)
        {
            return left / right;
        }
    }
}
