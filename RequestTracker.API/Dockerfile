

#should be reconfigured to use windows image, 
#or change System.Drawing.Common to something linux supported


	FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
	USER app
	WORKDIR /app
	EXPOSE 8080
	EXPOSE 8081

	FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
	ARG BUILD_CONFIGURATION=Release
	WORKDIR /src
	COPY ["RequestTracker.API/RequestTracker.API.csproj", "RequestTracker.API/"]
	RUN dotnet restore "./RequestTracker.API/RequestTracker.API.csproj"
	COPY . .
	WORKDIR "/src/RequestTracker.API"
	RUN dotnet build "./RequestTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

	FROM build AS publish
	ARG BUILD_CONFIGURATION=Release
	RUN dotnet publish "./RequestTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

	FROM base AS final
	WORKDIR /app
	COPY --from=publish /app/publish .
	ENTRYPOINT ["dotnet", "RequestTracker.API.dll"]



