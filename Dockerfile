#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Remove in production
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Profile_Service/Profile_Service.csproj", "Profile_Service/"]
RUN dotnet restore "Profile_Service/Profile_Service.csproj"
COPY . .
WORKDIR "/src/Profile_Service"
RUN dotnet build "Profile_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile_Service.dll"]