# BarberAPI

API RESTful desenvolvida em **ASP.NET Core (.NET 10)** para gerenciamento de barbearias, permitindo o cadastro de clientes, serviços e agendamentos, além de autenticação e autorização de usuários utilizando **ASP.NET Identity** e **JWT**.

O projeto foi construído com foco em boas práticas de arquitetura, separação de responsabilidades, segurança e escalabilidade, estando pronto para consumo por aplicações frontend ou mobile.

---

## 🚀 Funcionalidades

- Cadastro e gerenciamento de clientes
- Cadastro e gerenciamento de serviços
- Agendamento de serviços
- Autenticação de usuários
- Autorização via JWT Token
- Proteção de endpoints com `Authorize`
- Documentação interativa da API com Swagger

---

## 🛠️ Tecnologias Utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- ASP.NET Identity
- JWT (JSON Web Token)
- Swagger / OpenAPI
- Git

---

## 📁 Estrutura do Projeto

```text
BarberAPI
├── Controllers
│   ├── AgendamentoController.cs
│   ├── AutorizaController.cs
│   ├── ClienteController.cs
│   └── ServicoController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── DTO
│   ├── AgendamentoDTO.cs
│   ├── ClienteDTO.cs
│   ├── ServicoDTO.cs
│   └── UsuarioToken.cs
│
├── Models
│   ├── Agendamento.cs
│   ├── Cliente.cs
│   └── Servico.cs
│
├── Migrations
│
├── appsettings.json
├── Program.cs
└── BarberAPI.http
```
## 🔐 Autenticação e Autorização

A autenticação é realizada por meio do **ASP.NET Identity**, com geração de **JWT Token** durante o login.  
Os endpoints protegidos utilizam o atributo `Authorize`, garantindo acesso apenas a usuários autenticados.

---

## 🗄️ Persistência de Dados

- Mapeamento de entidades com **Entity Framework Core 10**
- Criação e versionamento do banco de dados via **Migrations**
- Relacionamento entre entidades
- Carregamento de dados relacionados utilizando `Include`

---

## 🧩 Boas Práticas Aplicadas

- Separação entre **Models** e **DTOs**
- Injeção de dependência
- Organização do código por responsabilidade
- Princípios de **Clean Code**
- Estrutura preparada para manutenção e evolução

---

## ▶️ Como Executar o Projeto

### 1. Clonar o repositório
```bash
git clone https://github.com/seu-usuario/BarberAPI.git
```
### 2. Configurar a string de conexão

Edite o arquivo `appsettings.json` com as credenciais do SQL Server.

---

### 3. Executar as migrations
```bash
dotnet ef database update
### 4. Executar a aplicação
```bash
dotnet run
```
### 5. Acessar o Swagger
```text
https://localhost:{porta}/swagger
```
## 📌 Observações

Este projeto contempla apenas o **backend**, estando preparado para integração com aplicações frontend ou mobile modernas.



