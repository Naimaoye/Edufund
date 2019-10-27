FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TrippleNT/TrippleNT.csproj", "TrippleNT/"]
RUN dotnet restore "TrippleNT/TrippleNT.csproj"
COPY . .
WORKDIR "/src/TrippleNT"
RUN dotnet build "TrippleNT.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TrippleNT.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrippleNT.dll"]