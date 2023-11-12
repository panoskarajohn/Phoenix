FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Artist/Artist.Api/Artist.Api.csproj", "Services/Artist/Artist.Api/"]
RUN dotnet restore "Services/Artist/Artist.Api/Artist.Api.csproj"
COPY . .
WORKDIR "/src/Services/Artist/Artist.Api"
RUN dotnet build "Artist.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Artist.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Artist.Api.dll"]
