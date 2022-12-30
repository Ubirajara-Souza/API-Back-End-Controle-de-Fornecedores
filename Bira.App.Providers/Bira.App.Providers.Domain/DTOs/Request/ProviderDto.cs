using System.ComponentModel.DataAnnotations;

namespace Bira.App.Providers.Domain.DTOs.Request
{
    public class ProviderDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Documento é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo Documento precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Document { get; set; }

        public int TypeProviders { get; set; }

        public AddressDto Address { get; set; }

        public bool Active { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

    }
}
