﻿# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["SearchAPI/SearchAPI.csproj", "SearchAPI/"]
# RUN dotnet restore "SearchAPI/SearchAPI.csproj"
# COPY . .
# WORKDIR "/src/SearchAPI"
# RUN dotnet build "SearchAPI.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "SearchAPI.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "SearchAPI.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base

WORKDIR /app

# COPY bin/Debug/net6.0 .

# RUN apt-get update \
#     && apt-get install -y --no-install-recommends \
#         libc6-dev \
#     && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SearchAPI/SearchAPI.csproj", "SearchAPI/"]
RUN dotnet restore "SearchAPI/SearchAPI.csproj"
COPY . .
WORKDIR "/src/SearchAPI"
RUN dotnet build "SearchAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchAPI.csproj" -c Release -o /app/publish

WORKDIR /app/publish
# ENTRYPOINT ["dotnet", "SearchAPI.dll"]

