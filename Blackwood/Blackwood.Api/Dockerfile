FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blackwood.Api/Blackwood.Api.csproj", "Blackwood.Api/"]
RUN dotnet restore "Blackwood.Api/Blackwood.Api.csproj"
COPY . .
WORKDIR "/src/Blackwood.Api"
RUN dotnet build "Blackwood.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blackwood.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blackwood.Api.dll"]
