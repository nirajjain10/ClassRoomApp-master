FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ClassroomApp.csproj", "src/"]
RUN dotnet restore "src/ClassroomApp.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "ClassroomApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClassroomApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClassroomApp.dll"]
