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