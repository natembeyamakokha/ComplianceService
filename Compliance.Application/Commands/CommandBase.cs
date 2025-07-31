namespace Compliance.Application.Commands
{
    public abstract record CommandBase : ICommand
    {
        public Guid Id { get; }

        protected CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }
    }

    public abstract record CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
    
}
