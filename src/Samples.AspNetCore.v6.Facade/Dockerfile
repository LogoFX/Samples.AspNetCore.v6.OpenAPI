#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Samples.AspNetCore.v6.Facade/Samples.AspNetCore.v6.Facade.csproj", "Samples.AspNetCore.v6.Facade/"]
RUN dotnet restore "Samples.AspNetCore.v6.Facade/Samples.AspNetCore.v6.Facade.csproj"
COPY . .
WORKDIR "/src/Samples.AspNetCore.v6.Facade"
RUN dotnet build "Samples.AspNetCore.v6.Facade.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Samples.AspNetCore.v6.Facade.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Samples.AspNetCore.v6.Facade.dll"]