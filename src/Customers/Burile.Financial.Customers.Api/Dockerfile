FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Customers/Burile.Financial.Customers.Api/Burile.Financial.Customers.Api.csproj", "src/Customers/Burile.Financial.Customers.Api/"]
RUN dotnet restore "src/Customers/Burile.Financial.Customers.Api/Burile.Financial.Customers.Api.csproj"
COPY . .
WORKDIR "/src/src/Customers/Burile.Financial.Customers.Api"
RUN dotnet build "Burile.Financial.Customers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Burile.Financial.Customers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Burile.Financial.Customers.Api.dll"]
