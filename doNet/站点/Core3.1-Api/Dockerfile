#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#方式一：编译、发布
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["站点/Core3.1-Api/Core31.WebApi.csproj", "站点/Core3.1-Api/"]
#COPY ["Infrastructure/Core.Common/Core.Common.csproj", "Infrastructure/Core.Common/"]
#COPY ["Infrastructure/Core.Model/Core.Model.csproj", "Infrastructure/Core.Model/"]
#RUN dotnet restore "站点/Core3.1-Api/Core31.WebApi.csproj"
#COPY . .
#WORKDIR "/src/站点/Core3.1-Api"
#RUN dotnet build "Core31.WebApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Core31.WebApi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Core31.WebApi.dll"]


#方式二：不编译、发布
#引入镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
 
#设定工作目录
WORKDIR /app
 
#在容器中程序使用的端口，一定要和程序启动使用的端口对应上
EXPOSE 80
 
#复制文件到工作目录
COPY . .
 
#启动程序
ENTRYPOINT ["dotnet", "Core31.WebApi.dll"]
