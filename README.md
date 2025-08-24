# template-api.vexia.com.br

Template base para microserviços .NET 8 da Vexia, seguindo Clean Architecture, com estrutura modular, separação de responsabilidades e prontuário para integração com AWS, Swagger, RabbitMQ, EF Core e JWT.

---

## 🧱 Camadas explicadas

### 🔹 `1-UI (WebApi)`

- Exposição dos endpoints REST.
- Swagger configurado.
- Autenticação JWT.
- Middleware de exceção e filtros globais.

### 🔹 `2-Application (WebApi.Application)`

- UseCases com responsabilidade única.
- Apenas 1 método público por UseCase (`Execute` ou `ExecuteAsync`).
- Pode chamar outros UseCases e Handlers.
- Possui configurações da aplicação.

### 🔹 `3-Domain (WebApi.Models)`

- DTOs, entidades, enums, interfaces, constantes.
- Não contém regras de negócio.
- É o "contrato" entre camadas.

### 🔹 `4-Infra (WebApi.Infra)`

- Implementa repositórios e serviços externos.
- Preparada para EF Core sem provider fixo.
- Integrações com APIs externas, Protheus, etc.

### 🔹 `5-Test (WebApi.Application.Test)`

- Testes unitários com xUnit e Moq.
- Foco em Application (UseCases).

### 🔹 `NativeInjector (WebApi.NativeInjector)`

- Central de injeção de dependência.
- Registra UseCases, repositórios, serviços externos, JWT, cache, AWS, etc.

---

## 🚀 Como usar este template

1. Clique em **Use this template** no GitHub.
2. Nomeie seu repositório (ex: `identity.vexia.com.br`).
3. Clone o novo repo:

```bash
git clone https://github.com/vexia-aplicacoes/identity.vexia.com.br.git
cd identity.vexia.com.br
```

4. Compile e execute:

```bash
dotnet restore
dotnet build
dotnet run --project 1-UI/WebApi
```

5. Acesse o Swagger:

```
https://localhost:{porta}/swagger
```

---

## 🔧 Personalização

- Altere nomes dos projetos e pastas (WebApi → IdentityApi).
- Renomeie a solução `.sln`.
- Atualize todos os namespaces.
- Configure o banco no `AppDbContext.cs` e `appsettings.json`.
- Registre seus UseCases e serviços reais.

Opcional: use o script `rename-template.ps1` para automatizar renomeação.

---

## 🌐 Publicação e domínios

- Documentação: `https://template-api.vexia.com.br`
- Exemplo de uso:
  - `https://identity.vexia.com.br`
  - `https://payment.vexia.com.br`

---

## ✨ Extras incluídos

- `README.md` completo
- `Dockerfile` multi-stage com imagem Alpine e suporte a pt-BR
- `appsettings.Template.json` como base inicial
- `rename-template.ps1` (opcional, em breve)
- `Directory.Build.props` para centralizar versões (opcional)

---

## 📁 Dockerfile de exemplo

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
RUN apk add --no-cache \
    libgdiplus --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted \
    icu-libs \
    tzdata
ENV TZ=America/Sao_Paulo \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["WebApi.Application/WebApi.Application.csproj", "WebApi.Application/"]
COPY ["WebApi.Models/WebApi.Models.csproj", "WebApi.Models/"]
COPY ["WebApi.Infra/WebApi.Infra.csproj", "WebApi.Infra/"]
COPY ["WebApi.NativeInjector/WebApi.NativeInjector.csproj", "WebApi.NativeInjector/"]
COPY ["global.json", "./"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
```

---

## 🔖 Checklist pós-clone

-

---

## 📄 Licença

Este repositório é privado e de uso interno da Vexia. Todos os direitos reservados.

