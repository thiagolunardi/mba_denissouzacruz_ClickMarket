# **ClickMarket - Aplicação de Mini Loja Virtual com MVC, SPA e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **ClickMarket**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
O objetivo principal é desenvolver uma aplicação de ecommerce que permite aos usuários criar, editar, visualizar e excluir produtos e categorias, tanto através de uma interface web utilizando MVC (Admin), quanto através de uma API RESTful e visualização da vitrine em SPA.


### **Autor(es)**
- **André Cesco**
- **Denis Cruz**
- **Rafhael Farias**
- **Roberio Pinto Souza**
- **Tiago Tavares**
- **José Renato de Oliveira**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para admin.
- **API RESTful:** Exposição dos recursos do ecommerce para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **SPA:** Exposição dos produtos cadastrados para acesso de usuários
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Blazor
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - ClickMarket.AppMvc/ - Projeto MVC
  - ClickMarket.Api/ - API RESTful
  - ClickMarket.Blazor/ - Projeto SPA
  - ClickMarket.Business/ - Serviços para acesso à camada de dados
  - ClickMarket.Data/ - Modelos de Dados e Configuração do EF Core
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para gestão de produtos, categorias e vendedores:** Permite criar, editar, visualizar e excluir produtos, categorias e vendedores. 
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **SPA:** Vitrine para exposição dos produtos criados pelos vendedores.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/denissouzacruz/ClickMarket`
   - `cd ClickMarket`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - No arquivo `appsettings.json`, para API, defina o local onde as imagens serão salvas. Por padrão, aponta para C:\temp.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/ClickMarket.AppMvc/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7050/
   - Para visualizar produtos carregados inicialmente, utilize o usuário: teste@teste.com e senha: Teste@123

4. **Executar a API:**
   - `cd src/ClickMarket.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7260/swagger

5. **Executar o SPA:**
   - `cd src/ClickMarket.Blazor/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7050/

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

https://localhost:7260/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
