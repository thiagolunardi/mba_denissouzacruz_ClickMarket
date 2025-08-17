using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.DotNet.Scaffolding.Shared;

namespace ClickMarket.AppMvc.Configurations
{
    public class MvcOptionsConfig
    {
        public static void ConfigurarMensagensModelBinding(DefaultModelBindingMessageProvider messageProvider)
        {
            messageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"O valor '{x}' não é válido para o campo {y}.");
            messageProvider.SetMissingBindRequiredValueAccessor(x => $"O valor para o campo {x} é obrigatório.");
            messageProvider.SetMissingKeyOrValueAccessor(() => "Um valor é obrigatório.");
            messageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "O corpo da requisição é obrigatório.");
            messageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => $"O valor '{x}' não é válido.");
            messageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido não é válido.");
            messageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O valor deve ser numérico.");
            messageProvider.SetUnknownValueIsInvalidAccessor(x => $"O valor fornecido não é válido para {x}.");
            messageProvider.SetValueIsInvalidAccessor(x => $"O valor fornecido não é válido para {x}.");
            messageProvider.SetValueMustBeANumberAccessor(x => $"O campo {x} deve ser numérico.");
            messageProvider.SetValueMustNotBeNullAccessor(x => $"O campo {x} não pode ser nulo.");
        }
    }
}
