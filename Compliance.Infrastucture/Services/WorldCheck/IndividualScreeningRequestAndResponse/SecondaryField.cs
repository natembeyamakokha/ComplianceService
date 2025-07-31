using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Compliance.Infrastructure.Services.WorldCheck.IndividualScreeningRequestAndResponse
{
    public class SecondaryField
    {
        [JsonPropertyName("typeId")]
        public string TypeId { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("dateTimeValue")]
        public string DateTimeValue { get; set; }

        // Factory method for value fields
        public static SecondaryField CreateValueField(string typeId, string value)
        {
            return new SecondaryField
            {
                TypeId = typeId,
                Value = value
            };
        }

        // Factory method for date fields
        public static SecondaryField CreateDateField(string typeId, string dateTimeValue)
        {
            return new SecondaryField
            {
                TypeId = typeId,
                DateTimeValue = dateTimeValue
            };
        }
    }
}
