FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server/LibrariansWorkplace.Web/LibrariansWorkplace.Web.csproj", "LibrariansWorkplace.Web/"]
COPY ["Server/Infrastructure/LibrariansWorkplace.Infrastructure.BL/LibrariansWorkplace.Infrastructure.BL.csproj", "Infrastructure/LibrariansWorkplace.Infrastructure.BL/"]
COPY ["Server/Domain/LibrariansWorkplace.Domain/LibrariansWorkplace.Domain.csproj", "Domain/LibrariansWorkplace.Domain/"]
COPY ["Server/Services/LibrariansWorkplace.Services.Interfaces/LibrariansWorkplace.Services.Interfaces.csproj", "Services/LibrariansWorkplace.Services.Interfaces/"]
COPY ["Server/Domain/LibrariansWorkplace.Domain.Interfaces/LibrariansWorkplace.Domain.Interfaces.csproj", "Domain/LibrariansWorkplace.Domain.Interfaces/"]
COPY ["Server/Infrastructure/LibrariansWorkplace.Infrastructure.Data/LibrariansWorkplace.Infrastructure.Data.csproj", "Infrastructure/LibrariansWorkplace.Infrastructure.Data/"]
RUN dotnet restore "LibrariansWorkplace.Web/LibrariansWorkplace.Web.csproj"
COPY . .
WORKDIR "Server/LibrariansWorkplace.Web"
RUN dotnet build "LibrariansWorkplace.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LibrariansWorkplace.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrariansWorkplace.Web.dll"]