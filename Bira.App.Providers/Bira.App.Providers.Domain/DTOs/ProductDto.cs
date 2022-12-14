using System.ComponentModel.DataAnnotations;

namespace Bira.App.Providers.Domain.DTOs
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Fornecedor é obrigatório")]
        public Guid ProviderId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo Descrição precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        public string ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateRegistration { get; set; }

        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        public string NameProvider { get; set; }

    }
}
