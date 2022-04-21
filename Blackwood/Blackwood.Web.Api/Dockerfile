FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blackwood.Web.Api/Blackwood.Web.Api.csproj", "Blackwood.Web.Api/"]
RUN dotnet restore "Blackwood.Web.Api/Blackwood.Web.Api.csproj"
COPY . .
WORKDIR "/src/Blackwood.Web.Api"
RUN dotnet build "Blackwood.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blackwood.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blackwood.Web.Api.dll"]
