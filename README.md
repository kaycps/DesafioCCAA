# 📚 Desafio CCAA - Catálogo de Livros

Projeto de uma API RESTful com autenticação, gerenciamento de livros e geração de relatório.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- xUnit + Moq (Testes)
- Swagger
- DDD (Domain-Driven Design)
- SOLID, Clean Architecture

---

## ⚙️ Primeiros Passos

### 🔧 1. Clonar o projeto
```bash
git clone https://github.com/kaycps/DesafioCCAA.git
cd seu-projeto

```
### 🔧 2. Modificar o Appsettings.json para se conectar a base
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DesafioCCAA;User Id=sa;Password=suasenha123;"
}
```
### 🔧 3. Acesse o terminal a partir do projeto DesafioCCAA.Infrastructure
```bash
execute o comando: dotnet ef database update

```
### 🔧 4. Inicio o projeto
```bash
dotnet run --project DesafioCCAA.API
ou
Defina o projeto DesafioCCAA.API e execute o projeto



