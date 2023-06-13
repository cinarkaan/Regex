using System.Text.RegularExpressions;

namespace RegularExpression.Task
{
    public class HumanTimeConverter
    {
        private static readonly Regex _hourRegex = new Regex(@"\d\d ?(?=:|-)");
        private static readonly Regex _minuteRegex = new Regex(@"(?<=:|-) ?\d\d");
        private static readonly Regex _monthWordRegex = new Regex(@"(?<= )[а-яі]+$");
        private static readonly Regex _dayRegex = new Regex(@"(?<= )\d\d?(?= [а-яі]+)");

        private Dictionary<string, Func<string, DateTime>> _convertingFunctionByDateTimePattern = new Dictionary<string, Func<string, DateTime>>
        {
            [@"\d{2}:\d{2}, (25 днів тому|25 days ago)$"] = (string input)
                => new DateTime(DateTime.Today.AddDays(-25).Year,
                    DateTime.Today.AddDays(-25).Month,
                    DateTime.Today.AddDays(-25).Day,
                    int.Parse(_hourRegex.Match(input).Value),
                    int.Parse(_minuteRegex.Match(input).Value),
                    0),

            [@"\d{2}:\d{2}, (2 дні тому|2 days ago)$"] = (string input)
                => new DateTime(DateTime.Today.AddDays(-2).Year,
                    DateTime.Today.AddDays(-2).Month,
                    DateTime.Today.AddDays(-2).Day,
                    int.Parse(_hourRegex.Match(input).Value),
                    int.Parse(_minuteRegex.Match(input).Value),
                    0), 

            [@"\d{2}:\d{2}, (Сьогодні|Сегодня|Today)$"] = (string input) =>
                new DateTime(DateTime.Today.Year,
                    DateTime.Today.Month,
                    DateTime.Today.Day,
                    int.Parse(_hourRegex.Match(input).Value),
                    int.Parse(_minuteRegex.Match(input).Value),
                    0),

            [@"\d{2}:\d{2}, (1 день тому|Вчора|Вчера|Yesterday)$"] = (string input)
                => new DateTime(DateTime.Today.AddDays(-1).Year,
                    DateTime.Today.AddDays(-1).Month,
                    DateTime.Today.AddDays(-1).Day,
                    int.Parse(_hourRegex.Match(input).Value),
                    int.Parse(_minuteRegex.Match(input).Value),
                    0),

            [@"\d{2}:\d{2}, \d{1,2} [а-яі]+$"] = (string input) =>
                new DateTime(DateTime.Today.Year,
                    _monthNumberByName.Single(i => i.Value.Contains(_monthWordRegex.Match(input).Value)).Key,
                    int.Parse(_dayRegex.Match(input).Value),
                    int.Parse(_hourRegex.Match(input).Value),
                    int.Parse(_minuteRegex.Match(input).Value),
                    0),
        };

        public DateTime Convert(string dateTime)
        {
            var functionToConvertDate = _convertingFunctionByDateTimePattern.Single(i => new Regex(i.Key).IsMatch(dateTime)).Value;

            var result = functionToConvertDate(dateTime);

            return result;
        }

        private static Dictionary<int, IEnumerable<string>> _monthNumberByName = new Dictionary<int, IEnumerable<string>>
        {
            [3] = new[] { "березня", "March" },
            [4] = new[] { "квітня", "April" },
        };
    }
}
