using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            string opName;
            bool usesSecondOperand = true;

            switch (op)
            {
                case "1":
                    result = num1 + num2;
                    opName = "Add";
                    break;
                case "2":
                    result = num1 - num2;
                    opName = "Subtract";
                    break;
                case "3":
                    result = num1 * num2;
                    opName = "Multiply";
                    break;
                case "4":
                    if (num2 != 0) result = num1 / num2;
                    opName = "Divide";
                    break;
                case "5":
                    result = Math.Sqrt(num1);
                    opName = "Square Root";
                    usesSecondOperand = false;
                    break;
                case "6":
                    result = Math.Pow(num1, num2);
                    opName = "Power";
                    break;
                case "7":
                    result = Math.Pow(10, num1);
                    opName = "10^n";
                    usesSecondOperand = false;
                    break;
                case "sin":
                    result = Math.Sin(ToRadians(num1));
                    opName = "Sine";
                    usesSecondOperand = false;
                    break;
                case "cos":
                    result = Math.Cos(ToRadians(num1));
                    opName = "Cosine";
                    usesSecondOperand = false;
                    break;
                case "tan":
                    result = Math.Tan(ToRadians(num1));
                    opName = "Tangent";
                    usesSecondOperand = false;
                    break;
                default:
                    // Unknown op:
                    // fail before anything is written to the log
                    throw new ArgumentException($"Unknown operation: {op}");
            }

            writer.WriteStartObject();
            writer.WritePropertyName("Operation");
            writer.WriteValue(opName);
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            if (usesSecondOperand)
            {
                writer.WritePropertyName("Operand2");
                writer.WriteValue(num2);
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            UsageCounter.IncreaseUsageCounter();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180;

    }
}
