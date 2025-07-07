using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace ClickMarket.Business.Services;

public abstract class BaseService(INotificador notificador)
{
    private readonly INotificador _notificador = notificador;

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : EntityBase
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }
}