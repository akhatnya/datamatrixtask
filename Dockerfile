FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataMatrixCorpTask/DataMatrixCorpTask.csproj", "DataMatrixCorpTask/"]
RUN dotnet restore "DataMatrixCorpTask/DataMatrixCorpTask.csproj"
COPY . .
WORKDIR "/src/DataMatrixCorpTask"
RUN dotnet build "DataMatrixCorpTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataMatrixCorpTask.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataMatrixCorpTask.dll"]