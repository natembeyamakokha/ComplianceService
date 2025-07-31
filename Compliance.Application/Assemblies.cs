using System.Reflection;
using Compliance.Application.Commands;

namespace Compliance.Application
{   
    public static class Assemblies
    {
        public static readonly Assembly Application = typeof(CommandBase).Assembly;
    }
}
