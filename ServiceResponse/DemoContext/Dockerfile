FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DemoContext/DemoContext.csproj", "DemoContext/"]
RUN dotnet restore "DemoContext/DemoContext.csproj"
COPY . .
WORKDIR "/src/DemoContext"
RUN dotnet build "DemoContext.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoContext.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoContext.dll"]
