using FluentValidation;
using FluentValidation.Results;

namespace ClickMarket.Business.Requests;

public class ClienteRequest : RequestBase
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public bool Ativo { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new ClienteValidation().Validate(this);

        return ValidationResult.IsValid;
    }

    private sealed class ClienteValidation : AbstractValidator<ClienteRequest>
    {
        public ClienteValidation()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(m => m.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} não pode exceder {MaxLength} caracteres");
            RuleFor(m => m.Email)
                .EmailAddress()
                .WithMessage("O campo {PropertyName} não é um e-mail válido")
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} não pode exceder {MaxLength} caracteres");
            RuleFor(m => m.Telefone)
                .MaximumLength(15)
                .WithMessage("O campo {PropertyName} não pode exceder {MaxLength} caracteres");
        }
    }
}
