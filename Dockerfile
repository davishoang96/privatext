FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM base AS final
WORKDIR /app
COPY ./publish .

ENTRYPOINT ["dotnet", "privatext.dll"]
