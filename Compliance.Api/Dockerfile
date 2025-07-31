FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8061

ENV ASPNETCORE_URLS=http://+:8061


RUN addgroup --gid 5678 appgroup \
    && adduser --uid 5678 --gid 5678 --disabled-password --gecos "" appuser \
    && mkdir -p /app \
    && chown -R appuser:appgroup /app

USER appuser


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Compliance.Api/Compliance.Api.csproj", "Compliance.Api/"]
COPY ["Compliance.Application/Compliance.Application.csproj", "Compliance.Application/"]
COPY ["Compliance.Domain/Compliance.Domain.csproj", "Compliance.Domain/"]
COPY ["Compliance.Infrastucture/Compliance.Infrastructure.csproj", "Compliance.Infrastucture/"]
COPY ["Compliance.Shared/Compliance.Shared.csproj", "Compliance.Shared/"]
COPY ["nuget.config", "."]

#Use vss access token to access nuget.
RUN wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash
ARG FEED_ACCESSTOKEN
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/ICT-DevOps/Omni-Channel/_packaging/OmniChannel/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"

RUN dotnet restore "Compliance.Api/Compliance.Api.csproj"
COPY . .
WORKDIR "/src/Compliance.Api"
RUN dotnet build "Compliance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compliance.Api.csproj" -c Release -o /app/publish 

FROM base AS final
ENV TZ=UTC
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compliance.Api.dll"]