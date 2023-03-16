FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DemoProject/DemoProject.csproj", "DemoProject/"]
RUN dotnet restore "DemoProject/DemoProject.csproj"
COPY . .
WORKDIR "/src/DemoProject"
RUN dotnet build "DemoProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoProject.dll"]
