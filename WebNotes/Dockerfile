#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["WebNotes/WebNotes.csproj", "WebNotes/"]
RUN dotnet restore "WebNotes/WebNotes.csproj"
COPY . .
WORKDIR "/src/WebNotes"
RUN dotnet build "WebNotes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebNotes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebNotes.dll"]
