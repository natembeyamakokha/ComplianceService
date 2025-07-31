namespace Compliance.Shared.Domains
{
    using System;
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }

    public interface IBusinessRule<T> where T : BaseEntity<Guid>
    {
        bool IsBroken();
        string Message { get; }
    }
}
