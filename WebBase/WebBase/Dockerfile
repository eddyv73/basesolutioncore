
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ENV User "email@email.com"
ENV Pwd "Password"
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebBase/WebBase.csproj", "WebBase/"]
RUN dotnet restore "WebBase/WebBase.csproj"
COPY . .
WORKDIR "/src/WebBase"
RUN dotnet build "WebBase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBase.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBase.dll"]