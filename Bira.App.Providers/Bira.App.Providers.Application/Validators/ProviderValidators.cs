using Bira.App.Providers.Application.Validators.Documents;
using Bira.App.Providers.Domain.Entities;
using Bira.App.Providers.Domain.Enums.Types;
using FluentValidation;

namespace Bira.App.Providers.Application.Validators
{
    public class ProviderValidators : AbstractValidator<Provider>
    {
        public ProviderValidators()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TypeProviders == TypeProviders.pessoaFisica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidators.SizeCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidators.Validate(f.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.TypeProviders == TypeProviders.pessoaJuridica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidators.SizeCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidators.Validate(f.Document)).Equal(true)
                     .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}
