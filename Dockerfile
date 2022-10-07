# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source
    
# Copy csproj and restore as distinct layers
COPY . .


RUN dotnet restore --disable-parallel
    
# Copy everything else and build

RUN dotnet publish -c Release -o /app-out --no-restore
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app-out
COPY --from=build /app-out ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "PAY-UP.Api.dll"]