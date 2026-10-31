FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CrumbCo.csproj", "./"]
RUN dotnet restore "CrumbCo.csproj"

COPY . .
RUN dotnet publish "CrumbCo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV PORT=3000
EXPOSE 3000

ENTRYPOINT ["dotnet", "CrumbCo.dll"]
