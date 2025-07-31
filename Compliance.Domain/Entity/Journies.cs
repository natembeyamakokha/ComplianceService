using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(Journies), Schema = "UTS")]
    public class Journies : BaseEntity<long>
    {
        [MaxLength(100)]
        public string Name { get; private set; }
        [MaxLength(20)]
        public string Code { get; private set; }

        private Journies(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public Journies() { }

        public static Journies Create(string name, string code)
        {
            return new Journies(name, code);
        }
    }
}