using System;

namespace SharpMath
{
    /// <summary>
    /// Generates random math problems for practice.
    /// </summary>
    internal static class ProblemGenerator
    {
        private const int MinDivisor = 1;
        private const int MinOperationType = 1;
        private const int MaxOperationType = 4;

        /// <summary>
        /// Generates a math problem based on the requested operation.
        /// </summary>
        /// <param name="operation">The operation type: '+', '-', '*', '/', or 'M' for mixed.</param>
        /// <param name="highNum">The maximum number allowed in the problem.</param>
        /// <param name="rng">Random number generator instance.</param>
        /// <returns>A tuple containing operands (x, y), the operation character, operation string, and the correct answer.</returns>
        public static (int x, int y, char op, string opString, int answer) GenerateProblem(char operation, int highNum, Random rng)
        {
            if (rng == null)
            {
                throw new ArgumentNullException(nameof(rng));
            }

            char currentOp = DetermineOperation(operation, rng);
            (int x, int y) = GenerateOperands(currentOp, highNum, rng);
            (string opString, int answer) = CalculateResult(currentOp, x, y);

            return (x, y, currentOp, opString, answer);
        }

        /// <summary>
        /// Determines the actual operation to use, handling mixed mode.
        /// </summary>
        private static char DetermineOperation(char operation, Random rng)
        {
            if (operation != 'M')
            {
                return operation;
            }

            // Mixed mode: pick one of the four operations uniformly
            int operationType = rng.Next(MinOperationType, MaxOperationType + 1);

            switch (operationType)
            {
                case 1:
                    return '+';
                case 2:
                    return '-';
                case 3:
                    return '*';
                case 4:
                    return '/';
                default:
                    return '+'; // Fallback
            }
        }

        /// <summary>
        /// Generates operands for the problem based on the operation type.
        /// </summary>
        private static (int x, int y) GenerateOperands(char operation, int highNum, Random rng)
        {
            if (operation == '/')
            {
                return GenerateDivisionOperands(highNum, rng);
            }

            int x = rng.Next(0, highNum + 1);
            int y = rng.Next(0, highNum + 1);

            // For subtraction, ensure x >= y to avoid negative results
            if (operation == '-' && y > x)
            {
                return (y, x);
            }

            return (x, y);
        }

        /// <summary>
        /// Generates operands for division ensuring integer quotient with no remainder.
        /// </summary>
        private static (int x, int y) GenerateDivisionOperands(int highNum, Random rng)
        {
            if (highNum <= 0)
            {
                // Fallback: 0 / 1 = 0 when highNum is 0 or negative
                return (0, 1);
            }

            // Generate divisor in range [1, highNum]
            int y = rng.Next(MinDivisor, highNum + 1);

            // Choose quotient q such that x = q * y <= highNum
            int maxQuotient = highNum / y;
            int quotient = maxQuotient > 0 ? rng.Next(0, maxQuotient + 1) : 0;
            int x = quotient * y;

            return (x, y);
        }

        /// <summary>
        /// Calculates the result and returns the operation string and answer.
        /// </summary>
        private static (string opString, int answer) CalculateResult(char operation, int x, int y)
        {
            switch (operation)
            {
                case '+':
                    return ("Plus", x + y);
                case '-':
                    return ("Minus", x - y);
                case '*':
                    return ("Times", x * y);
                case '/':
                    return ("Divided by", y != 0 ? x / y : 0);
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}", nameof(operation));
            }
        }
    }
}