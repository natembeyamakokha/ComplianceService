using Ardalis.SmartEnum;

namespace Compliance.Shared.Domains.Helper
{
    public class OrderByDirection : SmartEnum<OrderByDirection, string>
    {
        public static readonly OrderByDirection ASC = new(nameof(ASC), nameof(ASC));
        public static readonly OrderByDirection DESC = new(nameof(DESC), nameof(DESC));
        public OrderByDirection(string name, string value) : base(name, value)
        {
            
        }
    }
}