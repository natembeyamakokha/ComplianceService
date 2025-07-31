using Compliance.Shared.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance.Domain.Entity
{
    [Table(nameof(Channels), Schema = "UTS")]
    public class Channels : BaseEntity<long>
    {
        [MaxLength(50)]
        public string Name { get; private set; }
        [MaxLength(20)]
        public string Code { get; private set; }

        private Channels(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public Channels() { }

        public static Channels Create(string name, string code)
        {
            return new Channels(name, code);
        }
    }
}