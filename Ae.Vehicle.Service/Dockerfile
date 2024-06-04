
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ./ ./
ENTRYPOINT ["dotnet", "Ae.Vehicle.Service.dll"]