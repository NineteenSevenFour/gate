#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
RUN apt-get update && apt-get install -y curl  

WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ../dist/apps/gatehub/net7.0 .

HEALTHCHECK CMD curl --fail https://localhost:443/healthchecks || exit

ENTRYPOINT ["dotnet", "NineteenSevenFour.Gatehub.dll"]
