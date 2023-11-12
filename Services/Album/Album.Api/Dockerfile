FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Album/Album.Api/Album.Api.csproj", "Services/Album/Album.Api/"]
RUN dotnet restore "Services/Album/Album.Api/Album.Api.csproj"
COPY . .
WORKDIR "/src/Services/Album/Album.Api"
RUN dotnet build "Album.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Album.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Album.Api.dll"]
