namespace Compliance.Shared.Api
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using AspNet.Security.OpenIdConnect.Primitives;

    public class AuthUser : ClaimsPrincipal
    {
        public AuthUser(ClaimsPrincipal principal) : base(principal)
        {

        }

        public string GetClaimValue(string key)
        {
            if (this.Identity is not ClaimsIdentity identity)
                return null;

            var claim = this.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public string Username
        {
            get
            {
                if (this.FindFirst(OpenIdConnectConstants.Claims.Username) == null)
                    return String.Empty;

                return GetClaimValue(OpenIdConnectConstants.Claims.Username);
            }
        }

        public string CountryCode
        {
            get
            {
                if (this.FindFirst(AuthenticationClaims.COUNTRY_CODE) == null)
                    return String.Empty;

                return GetClaimValue(AuthenticationClaims.COUNTRY_CODE);
            }
        }

        public string CustomerId
        {
            get
            {
                if (this.FindFirst(AuthenticationClaims.CUSTOMER_ID) == null)
                    return String.Empty;

                return GetClaimValue(AuthenticationClaims.CUSTOMER_ID);
            }
        }

        public int UserId
        {
            get
            {
                if (FindFirst(OpenIdConnectConstants.Claims.Subject) == null)
                    return 0;

                return Int32.Parse(GetClaimValue(OpenIdConnectConstants.Claims.Subject));
            }
        }


        public string Source
        {
            get
            {
                if (Identity is not ClaimsIdentity identity)
                    return null;
                var claim = this.Claims.FirstOrDefault(c => c.Type == AuthenticationClaims.SOURCE);
                return claim?.Value;
            }
        }

        public string CustomerLevel
        {
            get
            {
                if (Identity is not ClaimsIdentity identity)
                    return null;
                var claim = this.Claims.FirstOrDefault(c => c.Type == AuthenticationClaims.CUSTOMER_LEVEL);
                return claim?.Value;
            }
        }

        public string ClientType
        {
            get
            {
                if (Identity is not ClaimsIdentity identity)
                    return null;
                var claim = Claims.FirstOrDefault(c => c.Type == AuthenticationClaims.CLIENT_TYPE);
                return claim?.Value;
            }
        }

        public string ClientId
        {
            get
            {
                if (Identity is not ClaimsIdentity identity)
                    return null;
                var claim = Claims.FirstOrDefault(c => c.Type == OpenIdConnectConstants.Claims.ClientId);
                return claim?.Value;
            }
        }
        public string BankId
        {
            get
            {
                if (Identity is not ClaimsIdentity identity)
                    return null;
                var claim = this.Claims.FirstOrDefault(c => c.Type == AuthenticationClaims.BANK_ID);
                return claim?.Value;
            }
        }
    }
    public class AuthenticationClaims
    {
        public const string SOURCE = "source";
        public const string Channel = "channel";
        public const string CLIENT_TYPE = "clientType";
        public const string CLIENT_ID = "clientId";
        public const string USERNAME = "username";
        public const string COUNTRY_CODE = "countryCode"; //  "countryCode": "UG",
        public const string CUSTOMER_ID = "customerId";
        public const string BANK_ID = "bankId";  // "bankId": 
        public const string CUSTOMER_LEVEL = "customerLevel";
        public const string USE_TRANSACTION_LIMIT = "useTransactionLimit";
    }
}
