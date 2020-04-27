// dotnet publish -r ubuntu.18.04-x64 -c Release --self-contained --no-dependencies /p:PublishSingleFile=true

using System;
using System.Numerics;
using System.Linq;

namespace Triangle
{
    public class TriangleProgram
    {
        static string WrongAmountMessage = "Wrong amount of arguments.";
        static string ZeroValueMessage = "Zero value is not allowed.";
        static string NegativeValueMessage = "Negative values are not allowed.";
        static string UnavailableSymbolsMessage = "Unavailable symbols detected.";
        static string TriangleDoesntExistMessage = "This triangle doesn't exist.";
        static string WrongArgumentMessageFormat = "{0} argument: {1}";
        static string TriangleTypeMessageFormat = "This triangle is {0}.";


        public static TriangleType GetTriangleType(BigInteger[] sides) {
            Func<BigInteger, BigInteger, BigInteger, bool> comp = (a, b, c) => a + b > c;
            var a = sides[0];
            var b = sides[1];
            var c = sides[2];
            if (!(comp(a,b,c) && comp(a,c,b) && comp(b,c,a)))
                return TriangleType.NotTriangle;
            
            if ( a == b && b == c )
                return TriangleType.Equilateral;
            else if ( a == b || b == c || c == a )
                return TriangleType.Isosceles;
            else
                return TriangleType.Scalene;
        }

        public static void PrintHelp() {
            Console.WriteLine("This program shows if a triangle is equilateral, isosceles or scalene.\nSides of triangle are to be provided in arguments:\n{program_name} {first_side} {second_side} {third_side}");
        }

        public static BigInteger[] ParseSides(string[] stringSides) {
            var sides = new BigInteger[3];

            for (int i = 0; i < 3; i++) {
                BigInteger value;
                if (!BigInteger.TryParse(stringSides[i], out value))
                    throw new ParseException(String.Format(WrongArgumentMessageFormat, i + 1, UnavailableSymbolsMessage));
                if (value == 0)
                    throw new ParseException(String.Format(WrongArgumentMessageFormat, i + 1, ZeroValueMessage));
                if (value < 0)
                    throw new ParseException(String.Format(WrongArgumentMessageFormat, i + 1, NegativeValueMessage));
                sides[i] = value;
            }
            return sides;
        }

        public static int Main(string[] args)
        {
            if (args == null || args.Length == 0 || args.Any(arg => arg == "help" || arg == "--help" || arg == "-h")) {
                PrintHelp();
                return 0;
            }
            if (args.Length != 3) {
                Console.WriteLine(WrongAmountMessage);
                return 0;
            }

            BigInteger[] sides;
            try {
                sides = ParseSides(args);
            } catch (ParseException e) {
                Console.WriteLine(e.Message);
                return 0;
            }
            
            var type = GetTriangleType(sides);

            if (type == TriangleType.NotTriangle)
                Console.WriteLine(TriangleDoesntExistMessage);
            else
                Console.WriteLine(TriangleTypeMessageFormat, Enum.GetName(typeof(TriangleType), type).ToLower());

            return 0;
        }
    }
}
