# ── Etapa 1: Build ──────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias primero (caching)
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código fuente y publicar
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ── Etapa 2: Runtime ────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "TiendaApp.dll"]