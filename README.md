```Docker
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Dotnet8-DockerProfile/Dotnet8-DockerProfile.csproj", "Dotnet8-DockerProfile/"]
RUN dotnet restore "./Dotnet8-DockerProfile/./Dotnet8-DockerProfile.csproj"
COPY . .
WORKDIR "/src/Dotnet8-DockerProfile"
RUN dotnet build "./Dotnet8-DockerProfile.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Dotnet8-DockerProfile.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet8-DockerProfile.dll"]
```
![image](https://github.com/akshayblevel/Dotnet8-DockerProfile/assets/38757471/9bf31726-9356-419e-afce-24b2caae26e1)

localhost:32769/swagger/index.html
![image](https://github.com/akshayblevel/Dotnet8-DockerProfile/assets/38757471/4dcf8753-c687-4849-ac1d-9308cd07170e)

![image](https://github.com/akshayblevel/Dotnet8-DockerProfile/assets/38757471/39a50620-8dc4-4790-94ef-d156f20f19da)

![image](https://github.com/akshayblevel/Dotnet8-DockerProfile/assets/38757471/8bf72717-afd9-4b39-ad51-8b28ebb04e8e)
