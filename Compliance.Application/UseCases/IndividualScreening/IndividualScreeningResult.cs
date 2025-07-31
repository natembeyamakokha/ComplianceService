using Compliance.Shared;


﻿namespace Compliance.Application.UseCases.IndividualScreening
{
    public class IndividualScreeningResult
    {
        public int Code { get; set; }
        public string CaseId { get; set; }
        public string CaseSystemId { get; set; }
        public string Result { get; set; }
        public bool Successful { get; set; }
        public string StatusMessage { get; set; }
        public string StatusCode { get; set; }
    }
}
