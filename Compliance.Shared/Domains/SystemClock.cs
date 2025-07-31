namespace Compliance.Shared.Domains
{
    using System;
    using TimeZoneConverter;

    public static class SystemClock
    {
        private static DateTime? _customDate;
        private static string _countryCode = "KE";
        public static void SetCountryCode(string countryCode)
        {
            _countryCode = countryCode?.ToUpperInvariant();
        }
        public static DateTime Now
        {
            get
            {
                if (_customDate.HasValue)
                {
                    return _customDate.Value;
                }

                else
                {

                    TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo(CountryZone(_countryCode));
                    DateTimeOffset currentLocalTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tzi);
                    return currentLocalTime.DateTime;
                }
            }
        }

        public static DateTime? Local
        {
            get
            {
                if (_customDate.HasValue)
                    return _customDate.Value;
                else
                {

                    TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Africa/Nairobi");
                    DateTimeOffset currentLocalTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tzi);
                    return currentLocalTime.DateTime;
                }

            }
        }
        public static void Set(DateTime customDate) => _customDate = customDate;

        public static void Reset() => _customDate = null;


        private static string CountryZone(string countryCode)
        {
            switch (countryCode)
            {
                case CountryCodeConstant.KE: return "Africa/Nairobi";
                case CountryCodeConstant.UG: return "Africa/Kampala";
                case CountryCodeConstant.SS: return "Africa/Juba";
                case CountryCodeConstant.CD: return "Africa/Nairobi";
                case CountryCodeConstant.RW: return "Africa/Kinshasa";
                case CountryCodeConstant.TZ: return "Africa/Dar_es_Salaam";

                default: return "Africa/Nairobi";
            }
        }
    }

    public class CountryCodeConstant
    {
        public const string KE = nameof(KE);
        public const string UG = nameof(UG);
        public const string SS = nameof(SS);
        public const string CD = nameof(CD);
        public const string RW = nameof(RW);
        public const string TZ = nameof(TZ);
    }
}
