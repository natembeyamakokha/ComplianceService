using System.Text.Json.Serialization;

namespace Compliance.Shared
{
    public class Result
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }
        [JsonPropertyName("successful")]
        public bool Successful => StatusCode == StatusCodes.SUCCESSFUL || StatusCode == "0";
    }

    public class Result<T>
    {
        #region properies
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonPropertyName("successful")]
        public bool Successful => StatusCode == StatusCodes.SUCCESSFUL;

        [JsonPropertyName("responseObject")]
        public T ResponseObject { get; private set; } = default;
        #endregion

        public static Result<T> Success(T instance, string message = "Successful") => new()
        {
            StatusCode = StatusCodes.SUCCESSFUL,
            StatusMessage = message,
            ResponseObject = instance
        };
        public static Result<T> Pending(string message, string code) => new()
        {
            StatusCode = code ?? StatusCodes.INVALID_REQUEST,
            StatusMessage = message
        };

        public static Result<T> Requires2FA(T instance, string message) => new()
        {
            StatusCode = StatusCodes.REQUIRE_2FA,
            StatusMessage = message,
            ResponseObject = instance
        };
        public static Result<T> Failure(string error = "Failed") => new()
        {
            StatusCode = StatusCodes.INVALID_REQUEST,
            StatusMessage = error
        };

        public static Result<T> Failure(T instance, string error = "Failed") => new()
        {
            StatusCode = StatusCodes.INVALID_REQUEST,
            StatusMessage = error,
            ResponseObject = instance
        };

    }

    public class StatusCodes
    {
        public const string SUCCESSFUL = "00";
        public const string REQUIRE_2FA = "05";
        public const string INSUFICIENT_BALANCE = "91";
        public const string INVALID_ACCOUNT_NUMBER = "92";
        public const string TRANSACTION_LIMIT_EXCEEDED = "93";
        public const string DUPLICATE_REQUEST = "94";
        public const string AUTHENTICATION_FAILED = "95";
        public const string UNABLE_TO_GET_CHARGES = "96";
        public const string INVALID_SIGNATURE = "98";
        public const string TRANSACTION_FAILED = "97";
        public const string INVALID_REQUEST = "99";
        public const string ALREADY_REGISTERED = "90";
        public const string PENDING_DOCUMENT_VERIFICATION = "79";
    }
}
