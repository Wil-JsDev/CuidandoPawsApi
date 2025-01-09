FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY CuidandoPawsApi.sln ./
COPY CuidandoPawsApi.Application/CuidandoPawsApi.Application.csproj ./CuidandoPawsApi.Application/
COPY CuidandoPawsApi.Domain/CuidandoPawsApi.Domain.csproj ./CuidandoPawsApi.Domain/
COPY CuidandoPawsApi.Infrastructure.Identity/CuidandoPawsApi.Infrastructure.Identity.csproj ./CuidandoPawsApi.Infrastructure.Identity/
COPY CuidandoPawsApi.Infrastructure.Persistence/CuidandoPawsApi.Infrastructure.Persistence.csproj ./CuidandoPawsApi.Infrastructure.Persistence/
COPY CuidandoPawsApi.Infrastructure.Shared/CuidandoPawsApi.Infrastructure.Shared.csproj ./CuidandoPawsApi.Infrastructure.Shared/
COPY CuidandoPawsApi.Infrastructure.Api/CuidandoPawsApi.Infrastructure.Api.csproj ./CuidandoPawsApi.Infrastructure.Api/
RUN dotnet restore

COPY . ./CuidandoPawsApi

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /app/CuidandoPawsApi
COPY --from=build /app/CuidandoPawsApi /app/CuidandoPawsApi
WORKDIR /app/CuidandoPawsApi
RUN dotnet publish -c Release -o /app/publish

FROM  mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5050
CMD [ "dotnet", "CuidandoPawsApi.Infrastructure.Api.dll" ]
