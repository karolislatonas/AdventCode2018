namespace AdventCodeSolution
{
    public static class StringExtensions
    {
        public static string TrimStart(this string toTrim, string trimValue)
        {
            var result = toTrim;

            while (result.StartsWith(trimValue))
            {
                result = result.Substring(trimValue.Length);
            }

            return result;
        }

        public static string TrimEnd(this string toTrim, string trimValue)
        {
            var result = toTrim;

            while (result.EndsWith(trimValue))
            {
                result = result.Substring(0, result.Length - trimValue.Length);
            }

            return result;
        }
    }
}
