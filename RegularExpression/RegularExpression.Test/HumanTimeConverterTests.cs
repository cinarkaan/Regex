using NUnit.Framework;
using RegularExpression.Task;
using System;
using System.Collections.Generic;

namespace RegularExpression.Test
{
    public class HumanTimeConverterTests
    {
        [TestCaseSource(nameof(TestData))]
        public void Convert_method(string input, DateTime expected)
        {
            var converter = new HumanTimeConverter();

            var actual = converter.Convert(input);

            Assert.AreEqual(expected, actual);
        }

        private static DateTime Now = DateTime.Now;

        public static IEnumerable<object[]> TestData = new[]
            {
                new object[] {"14:42, Сьогодні", new DateTime(Now.Year, Now.Month, Now.Day, 14, 42, 00) },
                new object[] {"14:14, Сьогодні", new DateTime(Now.Year, Now.Month, Now.Day, 14, 14, 00) },
                new object[] { "17:33, Вчора", new DateTime(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day, 17, 33, 00) },
                new object[] { "08:01, Вчора", new DateTime(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day, 08, 01, 00) },
                new object[] { "16:11, 2 квітня", new DateTime(Now.Year, 4, 2, 16, 11, 00) },
                new object[] { "17:31, 1 квітня", new DateTime(Now.Year, 4, 1, 17, 31, 00) },
                new object[] { "21:51, 31 березня", new DateTime(Now.Year, 3, 31, 21, 51, 00) },
                new object[] { "11:50, 2 дні тому", new DateTime(Now.AddDays(-2).Year, Now.AddDays(-2).Month, Now.AddDays(-2).Day, 11, 50, 00) },
                new object[] { "01:50, 1 день тому", new DateTime(Now.AddDays(-1).Year, Now.AddDays(-1).Month, Now.AddDays(-1).Day, 01, 50, 00) },
                new object[] { "01:50, 25 днів тому", new DateTime(Now.AddDays(-25).Year, Now.AddDays(-25).Month, Now.AddDays(-25).Day, 01, 50, 00) },
            };
    }
}
