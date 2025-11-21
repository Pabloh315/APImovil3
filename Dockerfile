FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./APImovil3/APImovil3.csproj ./APImovil3/
COPY ./APImovil3.sln .
RUN dotnet restore ./APImovil3.sln
COPY ./APImovil3/ ./APImovil3/
WORKDIR /src/APImovil3
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "APImovil3.dll"]

