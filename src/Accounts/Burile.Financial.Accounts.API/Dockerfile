FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Accounts/Burile.Financial.Accounts.Api/Burile.Financial.Accounts.Api.csproj", "src/Accounts/Burile.Financial.Accounts.Api/"]
RUN dotnet restore "src/Accounts/Burile.Financial.Accounts.Api/Burile.Financial.Accounts.Api.csproj"
COPY . .
WORKDIR "/src/src/Accounts/Burile.Financial.Accounts.Api"
RUN dotnet build "Burile.Financial.Accounts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Burile.Financial.Accounts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Burile.Financial.Accounts.Api.dll"]
