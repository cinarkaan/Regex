using System.Text.RegularExpressions;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {
            // "(^|\\s)" Whitespaces for the begin of string
            // "[ \f\t\v]+$" Whitespaces for the end of string

            var numberPattern = new Regex("^([^0-9]*)$"); // Whether find any number the statement which is given on me
            var spacesPattern = new Regex("\\b\\s+\\b|\\b\\s+@\\b|\\b@\\s+\\b|\\b.\\s+\\b|\\b\\s+.\\b"); // Whitespaces will be searched
            var emailPattern = new Regex("[.a-zA-Z]+@teaminternational.com$|[.a-zA-Z]+@teaminternational.com\\s+"); // E-mail will be verified whether valid or not

            var match = numberPattern.Match(input); 

            if (!match.Success) // For numbers
                return false;
            match = spacesPattern.Match(input);
            if (match.Success) // For whitespaces
                return false;
            match = emailPattern.Match(input);
            if (!match.Success) // For confirm email
                return false;
            else // No problem it's okay !!!
                return true;
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {

            List<string> resultSet = new List<string>();

            inputJson = Regex.Replace(inputJson, ",", ":"); // All commas will be turned into semicolon
            inputJson = Regex.Replace(inputJson, "{|}|\"", ""); // Remove unuseful chars

            var splitted = Regex.Split(inputJson, ":"); // Split string from each semicolons
            
            // Get fields
            for (int i = 0; i < splitted.Length; i += 2)
                resultSet.Add(splitted[i]);

            return resultSet;
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {
            List<string> resultSet = new List<string>();

            inputJson = Regex.Replace(inputJson, ",", ":"); // All commas will be turned into semicolon
            inputJson = Regex.Replace(inputJson, "{|}|\"", ""); // Remove unuseful chars

            var splitted = Regex.Split(inputJson, ":");// Split string from each semicolons
            // Get fields
            for (int i = 1; i < splitted.Length; i += 2)
                resultSet.Add(splitted[i]);

            return resultSet;

        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            List<string> resultSet = new List<string>();
            inputXml = Regex.Replace(inputXml, "<|>|/|\"|\\s+", ""); // Remove unuseful chars 
            MatchCollection matches = Regex.Matches(inputXml, "\\W*(FieldString)[0-9]\\W*|\\W*(FieldBool)\\W*|\\W*(FieldInt)\\W*|\\W*(_fieldString3)\\W*"); // Find tag names
            matches.ToList().ForEach(i => resultSet.Add(i.Value)); // Add each tag name into list
            resultSet = resultSet.Distinct().ToList(); // Remove repetitive strings from list
            return resultSet;
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            Console.WriteLine(inputXml);
            List<string> resultSet = new List<string>();
            string pattern = "<FieldString1>(.+?)<\\/FieldString1>|<FieldString2>(.+?)<\\/FieldString2>|<_fieldString3>(.+?)<\\/_fieldString3>|<FieldBool>(.+?)<\\/FieldBool>|<FieldInt>(.+?)<\\/FieldInt>";
            MatchCollection matches = Regex.Matches(inputXml, pattern); // Find all tag values in the xml
            resultSet.Add(matches[0].Groups[1].Value); // The group index of tag value will be found out by regex101.com website
            resultSet.Add(matches[1].Groups[3].Value); // The group index of tag value will be found out by regex101.com website
            resultSet.Add(matches[2].Groups[4].Value); // The group index of tag value will be found out by regex101.com website
            resultSet.Add(matches[3].Groups[5].Value); // The group index of tag value will be found out by regex101.com website
            return resultSet;

        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {

            List<string> numbers = new List<string>();

            input = Regex.Replace(input, ",|;", "|"); // Remove unuseful chars in order to explore stuff which is desired value
            
            Regex nonStart = new Regex("^(?!076|$)"); // Check out the string whether start with 076

            Regex ukranianNumberPattern = new Regex("^(?:([0-9])(?!\\1)){10}$|\\d{12}|.[^0-9]"); // Check out all phone numbers whether is suitable form

            var splitted = input.Split("|"); // Split string into substrings from this char

            // Check substrings with regex regulation
            foreach (var str in splitted)
            {
                if (ukranianNumberPattern.IsMatch(str) && nonStart.IsMatch(str)) // Check out the substrings
                    numbers.Add(str);
            }
            
            return numbers;
        }
    }
}