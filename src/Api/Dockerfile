#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used for VS debugging on Docker
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt install -y nodejs
WORKDIR /src
COPY ["src/Api/BerkeGaming.Api.csproj", "src/Api/"]
COPY ["src/Application/BerkeGaming.Application.csproj", "src/Application/"]
COPY ["src/Domain/BerkeGaming.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/BerkeGaming.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/Api/BerkeGaming.Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "BerkeGaming.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BerkeGaming.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BerkeGaming.Api.dll"]