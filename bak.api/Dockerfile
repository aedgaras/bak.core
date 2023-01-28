#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["bak.api/bak.api.csproj", "bak.api/"]
RUN dotnet restore "bak.api/bak.api.csproj"
COPY . .
WORKDIR "/src/bak.api"
RUN dotnet build "bak.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bak.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bak.api.dll",  "--server.urls", "https://*:5050"]