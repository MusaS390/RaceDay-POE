# Build and publish the MVC front end
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/RaceDay.Mvc/RaceDay.Mvc.csproj src/RaceDay.Mvc/
RUN dotnet restore src/RaceDay.Mvc/RaceDay.Mvc.csproj
COPY src/RaceDay.Mvc/ src/RaceDay.Mvc/
WORKDIR /src/src/RaceDay.Mvc
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Small runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","RaceDay.Mvc.dll"]
