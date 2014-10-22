using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Parser: IParser
    {
        private ICalculation _calculator = new Calculation();


        public int Calculate(List<string> dividedString)
        {
            int result = 0;
            List<Operation> operationsQueue = new List<Operation>();
            Operation currentOperation = new Operation();
            List<string> subOperations = new List<string>();
            int subResult = 0;
            bool newOperation = false;
            bool wasCalculations = false;
            for (int i = 0; i < dividedString.Count; i++)
            {
                // If number
                if (dividedString[i].Length > 1 || (dividedString[i].Length == 1 && Convert.ToChar(dividedString[i]) >= 48 && Convert.ToChar(dividedString[i]) <= 57))
                {
                    // Start new operation
                    if (!newOperation && dividedString[i] != "(")
                    {
                        if (!wasCalculations)
                        {
                            currentOperation.LeftArgument = Convert.ToInt32(dividedString[i]);
                            wasCalculations = true;
                        }
                        else
                        {
                            currentOperation.LeftArgument = result;
                        }
                        newOperation = true;
                    }
                    // If it is right argument of operation
                    else
                    {
                        if (i == dividedString.Count - 1)
                        {
                            currentOperation.RightArgument = Convert.ToInt32(dividedString[i]);
                        }
                        // If next operation is not mul, div or brackets
                        else if (dividedString[i + 1] != "*" && dividedString[i + 1] != "/" && dividedString[i + 1] != "(" &&
                            dividedString[i + 1] != ")")
                        {
                            newOperation = false;
                            currentOperation.RightArgument = Convert.ToInt32(dividedString[i]);
                        }
                        else
                        {
                            for (int j = i; j < dividedString.Count; j++)
                            {
                                // For brackets we should call this methid to calculate expression in brackets
                                if (dividedString[i + 1] == "(")
                                {
                                    int bracketIndex = 1;
                                    for (int k = j + 1; k < dividedString.Count; k++)
                                    {
                                        if (dividedString[k] == "(")
                                        {
                                            bracketIndex++;
                                        }
                                        else if (dividedString[k] == ")")
                                        {
                                            bracketIndex--;
                                        }
                                        else if (bracketIndex == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            subOperations.Add(dividedString[k]);
                                        }
                                    }
                                    subResult = Calculate(subOperations);
                                    subOperations.Clear();
                                    currentOperation.RightArgument = subResult;
                                    subResult = 0;
                                    break;
                                }
                                // If it is mul or div
                                else
                                {
                                    // Think if it will be a*(
                                    if (dividedString[i + 1] == "*" || dividedString[i + 1] == "/")
                                    {
                                        subOperations.Add(dividedString[i]);
                                        subOperations.Add(dividedString[i + 1]);
                                        subOperations.Add(dividedString[i + 2]);
                                        currentOperation.RightArgument = Calculate(subOperations);
                                        subOperations.Clear();
                                        i += 2;
                                        j += 2;
                                        //result = MathOperations(currentOperation);
                                        //break;
                                    }
                                }
                            }
                        }

                        // Here we must calculate
                        result = MathOperations(currentOperation);
                        newOperation = false;
                    }
                    
                }
                else
                {
                    if (!newOperation && dividedString[i] != "(")
                    {
                        currentOperation.LeftArgument = result;
                        newOperation = true;
                    }

                    if (dividedString[i] != "(" && dividedString[i] != ")")
                    {
                        if ((dividedString[i] == "*" || dividedString[i] == "/") && currentOperation.MathOperation != null)
                        {
                            subOperations.Add(currentOperation.RightArgument.ToString());
                            subOperations.Add(dividedString[i]);
                            subOperations.Add(dividedString[i+1]);

                            currentOperation.RightArgument = Calculate(subOperations);
                            subOperations.Clear();
                            result = MathOperations(currentOperation);
                            i += 2;
                            newOperation = false;
                            continue;
                        }
                        currentOperation.MathOperation = dividedString[i];
                    }
                    else //if (dividedString[i] == "(")
                    {
                        int subExperssionLength = 0;
                        for (int j = i; j < dividedString.Count; j++)
                        {
                            // For brackets we should call this methid to calculate expression in brackets
                            if (dividedString[i] == "(")
                            {
                                int bracketIndex = 1;
                                for (int k = j + 1; k < dividedString.Count; k++, subExperssionLength++)
                                {
                                    if (dividedString[k] == "(")
                                    {
                                        if (subOperations.Count > 0)
                                        {
                                            subOperations.Add(dividedString[k]);
                                        }
                                        bracketIndex++;
                                    }
                                    else if (dividedString[k] == ")")
                                    {
                                        bracketIndex--;
                                        if (bracketIndex == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            subOperations.Add(dividedString[k]);
                                        }
                                    }
                                    else
                                    {
                                        subOperations.Add(dividedString[k]);
                                    }
                                }
                                subResult = Calculate(subOperations);
                                subOperations.Clear();
                                if (subOperations.Count > 0)
                                {
                                    subOperations.Add(dividedString[i]);
                                }
                                currentOperation.RightArgument = subResult;
                                i += ++subExperssionLength;

                                if ( ((i + 1) <= (dividedString.Count - 1)) && (dividedString[i + 1] == "*" || dividedString[i + 1] == "/") && ((i + 2) <= dividedString.Count - 1 && dividedString[i + 2] != "("))
                                {
                                    subOperations.Add(currentOperation.RightArgument.ToString());
                                    subOperations.Add(dividedString[i + 1]);
                                    subOperations.Add(dividedString[i + 2]);
                                    currentOperation.RightArgument = Calculate(subOperations);
                                    subOperations.Clear();
                                    i += 2;
                                }
                                if (currentOperation.MathOperation != null)
                                {
                                    result = MathOperations(currentOperation);
                                    newOperation = false;
                                }
                                else
                                {
                                    currentOperation.LeftArgument = subResult;
                                    newOperation = true;
                                }
                                subResult = 0;
                                break;
                            }
                            // If it is mul or div
                            else
                            {
                                // Think if it will be a*(
                                if (dividedString[i + 1] != "(")
                                {
                                    subOperations.Add(dividedString[i]);
                                    subOperations.Add(dividedString[i + 1]);
                                    subOperations.Add(dividedString[i + 2]);
                                    currentOperation.RightArgument = Calculate(subOperations);
                                    subOperations.Clear();
                                    i += 2;
                                    j += 2;
                                }
                            }
                        }
                    }
                }
            }

            if (currentOperation.MathOperation == null)
            {
                result = currentOperation.RightArgument;
            }

            return result;
        }

        private int MathOperations(Operation currentOperation)
        {
            switch (currentOperation.MathOperation)
            {
                case "+":
                    return _calculator.Add(currentOperation.LeftArgument, currentOperation.RightArgument);
                case "-":
                    return _calculator.Sub(currentOperation.LeftArgument, currentOperation.RightArgument);
                case "*":
                    return _calculator.Mul(currentOperation.LeftArgument, currentOperation.RightArgument);
                case "/":
                    return _calculator.Div(currentOperation.LeftArgument, currentOperation.RightArgument);
                default:
                    return 0;
            }
        }
    }
}
