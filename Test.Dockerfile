FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY . .
RUN dotnet restore "AresFramework.GameEngine/AresFramework.GameEngine.csproj"
RUN dotnet test --no-restore