#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GetPatientAPI.csproj", ""]
RUN dotnet restore "./GetPatientAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GetPatientAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GetPatientAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetPatientAPI.dll"]
#CMD run -d -p 8080:80 getpatientapi