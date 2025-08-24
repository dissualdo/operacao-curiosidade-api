# Operação Curiosidade — Backend (.NET 8 + MySQL)

Este repositório contém a **API REST** do projeto **Operação Curiosidade**.  
A API fornece **autenticação** e **CRUD de usuários** com informações da “cebola” (fatos/dados, interesses, sentimentos, valores*).  

> O front-end (Angular 19 + Material) encontra-se em repositório separado.

⚠️ **Observação:** para agilizar a entrega, a atualização de usuário foi implementada como `POST /api/user/{id}` (em vez de `PUT/PATCH`). No README explicamos o trade-off.

 
---

## 📦 Requisitos
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [MySQL 8.x](https://dev.mysql.com/downloads/) (ou MariaDB compatível)
- Cliente SQL (Workbench, DBeaver, HeidiSQL, etc.)
- (Opcional) Visual Studio 2022 ou VS Code

---

## 🛠 Stack & Pacotes
- **.NET 8 Web API**
- **EF Core** + `Pomelo.EntityFrameworkCore.MySql`
- **Injeção de dependência** nativa do .NET
- **Logging** via `Microsoft.Extensions.Logging`

---

## ▶ Como rodar (passo a passo)

### 1) Instalar MySQL (XAMPP recomendado)
- Baixe/instale o XAMPP e habilite o MySQL.
- Confirme que o serviço está em execução (porta padrão `3306`).

### 2) Criar o banco
```sql
CREATE DATABASE operacao_curiosidade
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
