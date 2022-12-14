using Bira.App.Providers.Domain.Enums.Types;
using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Entities
{
    public class Provider : EntityBase
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public TypeProviders TypeProviders { get; set; }

        public Address Address { get; set; }

        public bool Active { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
