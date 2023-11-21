FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

#Here need to copy the projects in individual linux folder
COPY ["RestServer/RestServer.csproj","RestServer/"]
COPY ["Shared/Shared.csproj","RestServer/"]


RUN dotnet restore "RestServer/RestServer.csproj"
COPY . .
WORKDIR "/src/RestServer"
RUN dotnet build "RestServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","RestServer.dll"]

FROM build AS test
WORKDIR /
RUN dotnet restore "UnitTest/UnitTest.csproj" --configfile NuGet.Config
WORKDIR "/UnitTest"
RUN dotnet build "UnitTest.csproj" -c Release --no-restore -o /app/build