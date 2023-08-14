using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public static class Calculate
    {
        public static double DoCalculation(double a, double b, string operatorMath)
        {
            double result = a;

            switch (operatorMath)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "×":
                    result = a * b;
                    break;
                case "÷":
                    result = a / b;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

