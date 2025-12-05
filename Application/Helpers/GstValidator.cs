using System.Text.RegularExpressions;

namespace InvoicingAPI.Application.Helpers
{
    public static class GstValidator
    {
        private static readonly Regex GstRegex =
            new(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$",
                RegexOptions.Compiled);

        public static bool IsValid(string gst)
        {
            if (string.IsNullOrWhiteSpace(gst))
                return false;

            return GstRegex.IsMatch(gst.Trim().ToUpper());
        }
    }
}
