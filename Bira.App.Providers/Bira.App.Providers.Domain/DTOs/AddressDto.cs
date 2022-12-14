using System.ComponentModel.DataAnnotations;

namespace Bira.App.Providers.Domain.DTOs
{
    public class AddressDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Rua precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo Número precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Number { get; set; }

        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo Bairro precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo Cidade precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(2, ErrorMessage = "O campo CEP precisa ter {1} caracteres", MinimumLength = 2)]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo CEP precisa ter {1} caracteres", MinimumLength = 8)]
        public string ZipCode { get; set; }

        public Guid ProviderId { get; set; }
    }
}
