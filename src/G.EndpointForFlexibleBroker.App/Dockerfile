#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["G.EndpointForFlexibleBroker/G.EndpointForFlexibleBroker.csproj", "G.EndpointForFlexibleBroker/"]
RUN dotnet restore "G.EndpointForFlexibleBroker/G.EndpointForFlexibleBroker.csproj"
COPY . .
WORKDIR "/src/G.EndpointForFlexibleBroker"
RUN dotnet build "G.EndpointForFlexibleBroker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "G.EndpointForFlexibleBroker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "G.EndpointForFlexibleBroker.dll"]