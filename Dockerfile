FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY release .

ARG GAME_ENV
ARG SERVER_BUILD
ENV SERVER_BUILD=$SERVER_BUILD
ENV GAME_ENV=$GAME_ENV

RUN mkdir -p ~/.ares/Plugins
RUN mkdir -p ~/.ares/Cache
RUN mkdir -p ~/.ares/Data
RUN mkdir -p ~/.ares/Logs

RUN echo $SERVER_BUILD

ENTRYPOINT ["dotnet", "AresFramework.GameEngine.dll"]