# Feedback - Avaliação Final

## Funcionalidade 30%

Avalie se o projeto atende a todos os requisitos funcionais definidos.

* Todas a funcionalidades principais estão implementadas e funcionando conforme esperado.

## Qualidade do Código 20%

Considere clareza, organização e uso de padrões de codificação.

### Data
* Nos repositórios, ao utilizar `FirstOrDefaultAsync`, o retorno deve ser `nullable?`, exemplo:
```csharp
public async Task<Categoria?> GetByIdAsync(int id) => await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
```
* Evite usar `x` como nome de variável, use nomes mais descritivos.
* Em `IRepository` existe um `SaveChanges()`. Nao é responsabilidade do repositório salvar mudanças. Isso pertence à unidade de trabalho (Unit of Work).
* A abstração de repositório genérico não deve possuir métodos públicos para não oferecer meios de "esquivar" da especialização.
* A abstração `Repository<T>` espera `ClickDbContext` no construtor, mas o ideal é esperar `DbContext`, para facilitar testes e manutenção.
* Não foi adotado uma Unidade de Trabalho (Unit of Work) para gerenciar transações.
* Em `Repository.Remover` existe um `T objeto` onde `T` é uma entidade. Recomenda-se user `T entidade` para maior clareza, ou mesmo renomear `T` para `TEntity`.

### Business
* Em `EntityBase` o construtor deve ser `protected` para evitar instanciação direta.
* Os serviços estão fazendo _Dispose()_ dos repositórios incorretamente. Isso não é responsabilidade dos serviços já que existe um *Dependency Injector* gerenciando o ciclo de vida.
* Os serviços estão fazendo `GC.SupressFinalize(this)` incorretamente. Isso não é responsabilidade dos serviços já que isso _perturba_ o cicle do _Garbage Collector_.
* Em `CategoriaService` 'e usado contranstrutor primário, mas também contém campos privados desnecessários.
* Em `CategoriaService.Remover()`, traz a lista de produtos apenas para verificar se está vazia. Isso pode ser ineficiente. Recomenda-se usar `AnyAsync()` para essa verificação. O mesmo problema existe em `ClienteService.ObterPorId()`.
* O serviço `ProdutoService` gerencia `Favorito`, o que quebra a responsabilidade única. Recomenda-se criar um serviço específico para `Favorito`.
* O serviço `VendedorService` possui um método `InativarOuReativarAsync()` ou seja, duas responsabilidades em um único método. Recomenda-se separar em dois métodos distintos.
* As classes `IdentityMensagensPortugues` e `JwtSettings` estão sob `Extensions`, mas não são extensões.
* `CategoriaViewModel` está gerando um novo Id, mas isso é de responsabilidade da entidade de domínio. View models ou DTOs apenas transportam dados.
* Em `ProdutoViewModel` o nome da propriedade `NaListaDesejos` deve iniciar com `Esta` para seguir a convenção de nomenclatura.

### API
* Em `Program.cs`, a configuração de arquivos estáticos podem ser movidas para uma extensão de método para melhorar a organização.
* Em `DbMigrationsHelpersExtension` o método `UseDbMigrationsHelpers` deve ser assíncrono.
* _Controllers_ não devem acessar diretamente repositórios ou context de banco de dados. Eles devem interagir apenas com serviços.
* Os métodos `ProdutosController.SalvarImagem` e `ExcluirImagem` devem ser movidos para um serviço específico de gerenciamento de arquivos ou imagens.
* Em API, chamamos os tipos de entrada e saída de DTO (Data Transfer Object) ao invés de ViewModel, que é mais comum em aplicações MVC.

### AppMvc
* Existem _ViewModels_ gerando Id, mas isso é de responsabilidade da entidade de domínio.
* Existem pastas `ViewModels` e `Models`, mas dentro de `Models` há apenas uma `ViewModel`.
* _Controllers_ não devem acessar diretamente repositórios ou context de banco de dados. Eles devem interagir apenas com serviços.

### SPA
* Em `Program.cs`, a condifuração da API deve vir do `addsettings.json` e não ser hardcoded.
* As classes `CategoriaService` e `ProductService` são `Client`s de suas APIs, então o nome correto seria `CategoriaClient` ou `CategoriaApiClient`.
* Em `ProdutoService` há repetição de código:
  1. Obter token
  2. Assinalar _Auth Header_
  3. Fazer requisição HTTP
  4. Deserializar resposta para um tipo específico
  Recomenda-se criar um método genérico para isso e reutilizar.

### Geral
* Muitas classes possuem campos ou propriedades não utilizadas.
* Muitos arquivos possuem `using` não utilizados.
* Normalizar a nomenclatura de variáveis e métodos para seguir um padrão consistente - ou Português ou Inglês.
* Evitar comentários desnecessários que não agregam valor ao entendimento do código.
* Evitar código comentado. Se não for mais necessário, remova.


## Eficiência e Desempenho 20%

Avalie o desempenho e a eficiência das soluções implementadas.

* Existem algumas chamadas ao banco de dados que podem ser otimizadas, como mencionado na seção de Qualidade do Código.
* Existem algumas chamadas síncronas que podem ser assíncronas para melhorar a escalabilidade.


## Inovação e Diferenciais 10%

Considere a criatividade e inovação na solução proposta.

* A solução é sólida e atende bem aos requisitos, mas não apresenta inovações ou diferenciais significativos além do esperado.


## Documentação e Organização 10%

Verifique a qualidade e completude da documentação, incluindo README.md.

* A estrutura de diretórios não é refletida na estrutura do arquivo de solução `ClickMarket.sln`, deixando a navegação confusa.
* O projeto de `API` está sob o diretório de `FrontEnd`, apesar de ser um serviço de `Backend`.
* Existe uma pasta órfão em `./src/FrontEnd/wwwroot`.
* A executar `dotnet run` em qualquer projeto, ele iniciou na porta de `http`. Isso pois o perfil padrão é o `http`. Ao executar `dotnet run --launch-profile https`, ele iniciou `https` como documentado.
* Em **Executar o SPA**, o link para acessar a aplicação está incorreto, aponta para a documentação da API.
* Se vários projetos devem ser iniciados juntos, recomenda-se configurar um "Compound".
* Não especifica como ou qual projeto executar para o *database migration*
* Menciona `appsettings.json` mas não especifica de qual projeto
* Em parte da documentação é usado o termo "rodar projeto" em outras "executar projeto". Recomenda-se adotar "executar".
* Para arquivos temporários, recomenda-se usar o caminho padrão do Windows, `%Temp%`, que sempre aponta para `C:\Users\<Usuario>\AppData\Local\Temp`

Considerações:
* A solução de documentação é boa, mas pode ser melhorada com as correções acima.
* Consegui executar toda a solução com as informações fornecidas, mas tive que fazer algumas suposições.


## Resolução de Feedbacks 10%

Avalie a resolução dos problemas apontados na primeira avaliação de frontend

* Não havia pontos negativos na avaliação geral para serem corrigidos.
