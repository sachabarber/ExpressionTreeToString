﻿using System.Linq;
using Xunit;
using System.Linq.Dynamic.Core;
using ZSpitz.Util;
using System.Linq.Expressions;
using static ExpressionTreeToString.BuiltinRenderer;
using static System.Linq.Expressions.Expression;
using System.Linq.Dynamic.Core.Parser;
using static ExpressionTreeToString.Tests.Globals;

namespace ExpressionTreeToString.Tests {
    public class DynamicLinqTests {
        public static TheoryData<string, string, Expression> TestData =>
            Objects
                .Where(x => x.source == nameof(DynamicLinqTestObjects) && x.o is Expression)
                .SelectT((category, source, name, o) => (name, category, (Expression)o))
                .ToTheoryData();

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestMethod(string category, string name, Expression expr) {
            var selector = expr.ToString(DynamicLinq, "C#");
            var prm = Parameter(typeof(Person));
            var parser = new ExpressionParser(new[] { prm }, selector, new object[] { }, ParsingConfig.Default);

            // test that the generated string can be parsed successfully
            var _ = parser.Parse(null);
        }
    }
}
