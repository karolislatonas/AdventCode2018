using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCodeSolution.Day04.GuardActions
{
    public static class GuardActionParser
    {
        private static IReadOnlyDictionary<string, Func<(string time, string actionType), GuardAction>> actionParsers = 
            new Dictionary<string, Func<(string time, string action), GuardAction>>()
        {
            ["begins shift"] = ParseShiftBegins,
            ["falls asleep"] = ParseFallsAsleep,
            ["wakes up"] = ParseWakesUp
        };

        public static GuardAction ParseGuardAction(string input)
        {
            var timeAndAction = SplitInputToTimeAndAction(input);
            var actionType = FindActionType(timeAndAction.action);

            var parseAction = actionParsers[actionType];

            return parseAction(timeAndAction);
        }

        private static string FindActionType(string action)
        {
            return action.Split(' ').TakeLast(2).Aggregate((t, c) => $"{t} {c}");
        }

        private static BeginsShift ParseShiftBegins((string time, string action) input)
        {
            var match = Regex.Match(input.action, "#[0-9]+");

            var guardId = int.Parse(match.Value.TrimStart('#'));

            return new BeginsShift(guardId, ParseDateTime(input.time));
        }

        private static FallsAsleep ParseFallsAsleep((string time, string action) input)
        {
            return new FallsAsleep(ParseDateTime(input.time));
        }

        private static WakesUp ParseWakesUp((string time, string action) input)
        {
            return new WakesUp(ParseDateTime(input.time));
        }

        private static DateTime ParseDateTime(string input) => DateTime.ParseExact(input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        private static (string time, string action) SplitInputToTimeAndAction(string input)
        {
            var splittedInput = input.Split('[', ']')
                .WhereNotNullOrEmpty()
                .Select(v => v.Trim())
                .ToArray();

            return (splittedInput.First(), splittedInput.Last());
        }

        
    }
}
