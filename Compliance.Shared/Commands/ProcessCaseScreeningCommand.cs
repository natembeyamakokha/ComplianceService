using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Compliance.Shared.Commands
{
    public class ProcessCaseScreeningCommand : IRequest<string>
    {
        public string GroupId { get; set; }
        public string EntityType { get; set; }
        public List<string> ProviderTypes { get; set; }
        public string Name { get; set; }
    }

}
