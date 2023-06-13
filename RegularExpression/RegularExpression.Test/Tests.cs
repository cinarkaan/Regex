using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace RegularExpression.Test
{
    public class Tests
    {
        private TestClass _testInstanceToSerialize = new TestClass
        {
            FieldString1 = "field1",
            FieldString2 = null,
            _fieldString3 = "_field3",
            FieldBool = true,
            FieldInt = 42,
        };

        [TestCase("serhii.mykhailov@teaminternational.com", true)]
        [TestCase("serhii.mykhailov@teaminternational.com ", true)]
        [TestCase(" serhii.mykhailov@teaminternational.com", true)]
        [TestCase("serhii.mykhailov @teaminternational.com", false)]
        [TestCase("serhii.mykhailov@teaminternational.com1", false)]
        [TestCase("serhii1.mykhailov2@teaminternational1.com1", false)]
        [TestCase("serhii1mykhailov@teaminternational1.com1", false)]
        [TestCase("serhii.@teaminternational1.com1", false)]
        [TestCase(".mykhailov@teaminternational1.com1", false)]
        [TestCase("serhii.mykhailovteaminternational.com", false)]
        [TestCase("serhii.mykhailov@teaminternational.om", false)]
        [TestCase("serhii.mykhailov@teaminternational.comm ", false)]
        public void Test1(string input, bool expected)
        {
            var result = RegularExpressionStore.Method1(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test2()
        {
            var expected = _testInstanceToSerialize.GetType().GetFields().Select(i => i.Name);

            var result = RegularExpressionStore.Method2(JsonConvert.SerializeObject(_testInstanceToSerialize));
            
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test3()
        {
            var expected = _testInstanceToSerialize.GetType().GetFields().Select(i => i.GetValue(_testInstanceToSerialize));

            var result = RegularExpressionStore.Method3(JsonConvert.SerializeObject(_testInstanceToSerialize));
            Assert.AreEqual(expected.Select(i => i == null ? "null" : i.ToString()).Select(i => i.ToLower()), result);
        }

        [Test]
        public void Test4()
        {
            var expected = _testInstanceToSerialize.GetType().GetFields().Select(i => i.Name);

            var serializer = new XmlSerializer(typeof(TestClass));
            string serialized;

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, _testInstanceToSerialize);
                    serialized = sw.ToString(); 
                }
            }
            //expected.ToList().ForEach(i => Console.WriteLine(i));

            var result = RegularExpressionStore.Method4(serialized);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test5()
        {
            var expected = _testInstanceToSerialize.GetType().GetFields().Select(i => i.GetValue(_testInstanceToSerialize)).Where(i => i!= null);

            var serializer = new XmlSerializer(typeof(TestClass));
            string serialized;

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, _testInstanceToSerialize);
                    serialized = sw.ToString();
                }
            }

            var result = RegularExpressionStore.Method5(serialized);

            Assert.AreEqual(expected.Select(i => i.ToString()).Select(i => i.ToLower()), result);
        }

        [Test]
        public void Test6()
        {
            var input = "0951234567|+380681234567|+30681234567|(067)0000000|(067) - 666 - 99 - 00,(067)-123-45-67;067-123-45-67|+38 067 123 45 67|067.123.45.67,9999990000|06712345678,076 123 45 67,068 1234 567";

            var expected = new[]
            {
                "0951234567",
                "+380681234567",
                "(067)0000000",
                "(067) - 666 - 99 - 00",
                "(067)-123-45-67",
                "067-123-45-67",
                "+38 067 123 45 67",
                "067.123.45.67",
                "068 1234 567"
            };

            var result = RegularExpressionStore.Method6(input);

            Assert.AreEqual(expected, result);
        }
    }
}