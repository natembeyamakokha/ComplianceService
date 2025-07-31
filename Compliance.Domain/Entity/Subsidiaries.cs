using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(Subsidiaries), Schema = "UTS")]
    public class Subsidiaries : BaseEntity<long>
    {
        [MaxLength(50)]
        public string Name { get; private set; }

        [MaxLength(20)]
        public string Code { get; private set; }

        private Subsidiaries(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public Subsidiaries() { }

        public static Subsidiaries Create(string name, string code)
        {
            return new Subsidiaries(name, code);
        }
    }
}