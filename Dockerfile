# Etapa Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./APImovil3/APImovil3.csproj .
RUN dotnet restore

COPY ./APImovil3/ .
RUN dotnet publish -c Release -o /app

# Etapa Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "APImovil3.dll"]

