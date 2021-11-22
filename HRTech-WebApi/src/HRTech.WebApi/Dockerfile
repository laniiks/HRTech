FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . /apps
WORKDIR /apps/src/HRTech.WebApi/
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS runtime
WORKDIR /apps
COPY --from=build /apps/src/HRTech.WebApi/out ./
ENTRYPOINT ["dotnet", "HRTech.WebApi.dll"]