FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ./*.sln ./

# Copy the main source project files
COPY /*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p /${file%.*}/ && mv $file /${file%.*}/; done

RUN dotnet restore

COPY . .
RUN dotnet build -c Release --no-restore

FROM build AS publish
RUN dotnet publish "G.EndpointForFlexibleBroker.App/G.EndpointForFlexibleBroker.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "G.EndpointForFlexibleBroker.App.dll"]
