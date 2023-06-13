using System.Xml;
using System.Xml.Serialization;

namespace RegularExpression.Test
{
    public class TestClass
    {
        public string FieldString1;
        [XmlElement(IsNullable = true)]
        public string FieldString2;
        public string _fieldString3;
        public bool FieldBool;
        public int FieldInt;
    }
}