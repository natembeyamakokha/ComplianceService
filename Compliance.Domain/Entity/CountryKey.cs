using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Domain.Entity
{
    public class CountryKey
    {
        private static readonly Dictionary<string, string> Countries = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"KEN", "KENYA"}, {"AFG", "Afghanistan"}, /* ... all other countries ... */
            {"ZZZ", "Unknown"}
        };

        public static string GetCountryKey(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            foreach (var kvp in Countries)
            {
                if(string.Equals(RemoveDiacritics(kvp.Value), countryName, StringComparison.OrdinalIgnoreCase))
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        private static string RemoveDiacritics(string text)
        {
            // Using System.Globalization for diacritic removal
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
