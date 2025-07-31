using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Infrastructure.Domain.Configurations.Compliance
{
    public class ScreeningConfig
    {
        public string GroupId { get; set; }
        public string GroupScreeningType { get; set; }
        public List<string> MandatoryProviderTypes { get; set; }
        public NameTransposition NameTransposition { get; set; }
        public Dictionary<string, ProviderConfig> SecondaryFieldsByProvider { get; set; }
    }

    public class ProviderConfig
    {
        public Dictionary<string, List<SecondaryFieldConfig>> SecondaryFieldsByEntity { get; set; } = new();
    }

    public class SecondaryFieldConfig
    {
        public string TypeId { get; set; }
        public string FieldValueType { get; set; }
        public string RegExp { get; set; }
        public bool FieldRequired { get; set; }
        public string Label { get; set; }
    }

    public class NameTransposition
    {
        public bool Selected { get; set; }
        public string Type { get; set; }
        public bool Available { get; set; }
    }
}
