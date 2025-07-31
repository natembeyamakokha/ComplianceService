using FluentValidation.Results;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Compliance.Shared
{
    public class SimSwapException : Exception
    {
        public string StatusCode { get; }
        public string StatusMessage { get; set; }
        [Display(Name = "Message")]
        public string MainMessage { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; }
        public string FormattedError { get; }

        protected SimSwapException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SimSwapException(string message) : base(message)
        {
            StatusCode = StatusCodes.INVALID_REQUEST;
            StatusMessage = message;
            MainMessage = message;
        }

        public SimSwapException(List<ValidationFailure> failures)
            : this("One or more validation failures have occurred.")
        {
            ValidationErrors = new Dictionary<string, string[]>();
            IEnumerable<string> propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (string propertyName in propertyNames)
            {
                string[] propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                FormattedError += String.Join(",", failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)) + ", ";

                ValidationErrors.Add(propertyName, propertyFailures);
            }
        }
    }
}