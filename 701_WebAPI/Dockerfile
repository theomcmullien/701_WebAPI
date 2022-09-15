#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["701_WebAPI/701_WebAPI.csproj", "701_WebAPI/"]
RUN dotnet restore "701_WebAPI/701_WebAPI.csproj"
COPY . .
WORKDIR "/src/701_WebAPI"
RUN dotnet build "701_WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "701_WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "701_WebAPI.dll"]