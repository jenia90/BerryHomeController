FROM microsoft/dotnet:2.1.2-aspnetcore-runtime-stretch-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS http://+:80
RUN apt-get update; apt-get install -yq gnupg
RUN curl -sL https://deb.nodesource.com/setup_9.x | bash -
RUN apt-get update; apt-get install -yq nodejs build-essential curl libunwind8 gettext
RUN apt-get -yq clean; rm -rf /var/lib/apt/lists/*

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY BerryHomeController/BerryHomeController.Api/BerryHomeController.Api.csproj BerryHomeController/BerryHomeController.Api/
RUN dotnet restore BerryHomeController/BerryHomeController.Api/BerryHomeController.Api.csproj
COPY . .
WORKDIR /src/BerryHomeController/BerryHomeController.Api
RUN dotnet build BerryHomeController.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BerryHomeController.Api.csproj -c Release -o /app -r linux-arm

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BerryHomeController.Api.dll"]
