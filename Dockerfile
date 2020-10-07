FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app/source
COPY Server3/ .
COPY Library/ .
RUN dotnet restore
RUN dotnet publish "Server3.csproj" --configuration "Debug" --output "../build-server" --framework "netcoreapp3.1" --runtime "alpine.3.11-x64" --self-contained false --force

#=======

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS app-server
WORKDIR /app
COPY --from=build /app/build-server .
ENTRYPOINT ["dotnet", "Server3.dll"]

