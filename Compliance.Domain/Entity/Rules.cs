using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(Rules), Schema = "UTS")]
    public class Rules : BaseEntity<long>
    {
        [MaxLength(100)]
        public string Name { get; private set; }
        [MaxLength(20)]
        public string Code { get; private set; }

        private Rules(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public Rules() { }

        public static Rules Create(string name, string code)
        {
            return new Rules(name, code);
        }
    }
}