FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AresFramework.WebApi/AresFramework.WebApi.csproj", "AresFramework.WebApi/"]
RUN dotnet restore "AresFramework.WebApi/AresFramework.WebApi.csproj"
COPY . .
WORKDIR "/src/AresFramework.WebApi"
RUN dotnet build "AresFramework.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AresFramework.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AresFramework.WebApi.dll"]
