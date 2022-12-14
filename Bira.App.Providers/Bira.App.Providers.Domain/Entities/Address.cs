using Bira.App.Providers.Domain.Package;

namespace Bira.App.Providers.Domain.Entities
{
    public class Address : EntityBase
    {
        public Guid ProviderId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public Provider Provider { get; set; }
    }
}
