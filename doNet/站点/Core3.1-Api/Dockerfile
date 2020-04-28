#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["站点/Core3.1-Api/Core31.WebApi.csproj", "站点/Core3.1-Api/"]
COPY ["Infrastructure/Core.Common/Core.Common.csproj", "Infrastructure/Core.Common/"]
COPY ["Infrastructure/Core.Model/Core.Model.csproj", "Infrastructure/Core.Model/"]
RUN dotnet restore "站点/Core3.1-Api/Core31.WebApi.csproj"
COPY . .
WORKDIR "/src/站点/Core3.1-Api"
RUN dotnet build "Core31.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core31.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core31.WebApi.dll"]