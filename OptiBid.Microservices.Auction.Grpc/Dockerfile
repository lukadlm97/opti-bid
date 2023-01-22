#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OptiBid.Microservices.Auction.Grpc/OptiBid.Microservices.Auction.Grpc.csproj", "OptiBid.Microservices.Auction.Grpc/"]
RUN dotnet restore "OptiBid.Microservices.Auction.Grpc/OptiBid.Microservices.Auction.Grpc.csproj"
COPY . .
WORKDIR "/src/OptiBid.Microservices.Auction.Grpc"
RUN dotnet build "OptiBid.Microservices.Auction.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OptiBid.Microservices.Auction.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OptiBid.Microservices.Auction.Grpc.dll"]