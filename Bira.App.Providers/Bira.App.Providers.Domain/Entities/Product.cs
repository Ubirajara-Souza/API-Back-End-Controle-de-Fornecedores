using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Entities
{
    public class Product : EntityBase
    {
        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Value { get; set; }

        public DateTime DateRegistration { get; set; }

        public bool Active { get; set; }

        public Provider Provider { get; set; }
    }
}
