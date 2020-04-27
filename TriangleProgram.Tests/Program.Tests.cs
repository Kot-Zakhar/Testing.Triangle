using System;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Numerics;

namespace Triangle.Tests
{
    public class TriangleTests
    {
        private readonly ITestOutputHelper output;

        public TriangleTests(ITestOutputHelper output) {
            this.output = output;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1")]
        [InlineData("2", "2")]
        [InlineData("3", "3", "3")]
        [InlineData("4", "4", "4", "4")]
        public void Program_DifferentAmountOfParams_Returns0(params string[] args)
        {
            output.WriteLine(args != null ? string.Join(", ", args) : "no args");
            var result = TriangleProgram.Main(null);
            Assert.Equal(0, result);
        }

        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-1.1")]
        [InlineData("0", "1", "2")]
        [InlineData("1", "0", "2")]
        [InlineData("2", "1", "0")]
        [InlineData("0", "0", "2")]
        [InlineData("1", "0", "0")]
        [InlineData("0", "0", "0")]
        public void ParseSides_ZeroSide_ThrowsParseException(params string[] sides) =>
            ParseSides_WrongValue_ThrowsParseException(sides);
        
        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-1.2")]
        [InlineData("-1",  "1",  "2")]
        [InlineData( "1", "-1",  "2")]
        [InlineData( "2",  "1", "-1")]
        [InlineData("-1", "-1",  "2")]
        [InlineData( "1", "-1", "-1")]
        [InlineData("-1", "-1", "-1")]
        public void ParseSides_NegativeValue_ThrowsParseException(params string[] sides) =>
            ParseSides_WrongValue_ThrowsParseException(sides);

        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-1.3")]
        [InlineData("a", "1", "2")]
        [InlineData("1", "a1", "2")]
        [InlineData("2", "1", "1a")]
        [InlineData("2a3", "a", "2")]
        [InlineData("1", "a", "a")]
        [InlineData("a", "a", "a")]
        public void ParseSides_TextValue_ThrowsParseException(params string[] sides) =>
            ParseSides_WrongValue_ThrowsParseException(sides);

        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-1.4")]
        [InlineData("2.1", "1", "2")]
        [InlineData("1", "0.1", "2")]
        [InlineData("2", "1", "3.44")]
        [InlineData("2.", "1.2", "2")]
        [InlineData("1", "0.1", "0.001")]
        [InlineData("00.01", "0.1", "0.01")]
        public void ParseSides_FloatSide_ThrowsParseException(params string[] sides) =>
            ParseSides_WrongValue_ThrowsParseException(sides);
        
        public ParseException ParseSides_WrongValue_ThrowsParseException(string[] sides) {
            output.WriteLine(sides != null ? string.Join(", ", sides) : "no args");
            return Assert.Throws<ParseException>(() => TriangleProgram.ParseSides(sides));
        }

        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-2")]
        [InlineData("1", "2", "3")]
        [InlineData("1", "2", "5")]
        [InlineData("1", "3", "2")]
        [InlineData("1", "5", "2")]
        [InlineData("3", "1", "2")]
        [InlineData("5", "1", "2")]
        public void GetTriangleType_NotTriangle_ReturnsNotTriangleType(params string[] sides) {
            BigInteger[] parsedSides = sides.Select(side => BigInteger.Parse(side)).ToArray();
            TriangleType resultType = TriangleProgram.GetTriangleType(parsedSides);
            Assert.Equal(Triangle.TriangleType.NotTriangle, resultType);
        }

        [Theory]
        [Trait("Category", "Functional requirements")]
        [Trait("Number", "FR-3")]
        [InlineData(TriangleType.Equilateral, "3", "3", "3")]
        [InlineData(TriangleType.Isosceles, "1", "2", "2")]
        [InlineData(TriangleType.Isosceles, "2", "1", "2")]
        [InlineData(TriangleType.Isosceles, "2", "2", "1")]
        [InlineData(TriangleType.Scalene, "3", "4", "2")]
        public void GetTriangleType_ValidTriangle_ReturnsTriangleType(Triangle.TriangleType type, params string[] sides) {
            BigInteger[] parsedSides = sides.Select(side => BigInteger.Parse(side)).ToArray();
            TriangleType resultType = TriangleProgram.GetTriangleType(parsedSides);
            Assert.Equal(type, resultType);
        }
    }
}
