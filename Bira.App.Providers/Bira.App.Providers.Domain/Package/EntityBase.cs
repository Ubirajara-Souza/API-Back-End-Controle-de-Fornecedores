using System.ComponentModel.DataAnnotations;

namespace Bira.App.Providers.Domain.Package
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}

