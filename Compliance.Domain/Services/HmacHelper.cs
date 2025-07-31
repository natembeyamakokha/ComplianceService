using System.Security.Cryptography;
using System.Text;

namespace Compliance.Domain.Services
{
    public static class HmacHelper
    {
        public static string GenerateSignature(string secretKey, string dataToSign)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string BuildDataToSign(string httpMethod, string requestUri, string date, string contentType, string contentLength, string body)
        {
            return $"(request-target): {httpMethod.ToLower()} {requestUri}\n" +
                   $"date: {date}\n" +
                   $"content-type: {contentType}\n" +
                   $"content-length: {contentLength}\n" +
                   $"{body}";
        }

        public static bool VerifySignature(string secretKey, string dataToSign, string signatureToVerify)
        {
            var generatedSignature = GenerateSignature(secretKey, dataToSign);
            return generatedSignature.Equals(signatureToVerify, StringComparison.OrdinalIgnoreCase);
        }
    }
}