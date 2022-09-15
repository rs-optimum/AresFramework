FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY . .
RUN dotnet restore "AresFramework.GameEngine/AresFramework.GameEngine.csproj"

FROM build-env AS publish
RUN dotnet publish -c Release -o release


FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=publish /app/release .

ENV SERVER_BUILD ${SERVER_BUILD}

RUN mkdir -p ~/.ares/Plugins
RUN mkdir -p ~/.ares/Cache

ENTRYPOINT ["dotnet", "AresFramework.GameEngine.dll"]