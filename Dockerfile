FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /ApiGateway
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /OcelotApiGw
COPY . .

RUN dotnet restore -s http://10.10.1.16:4100 -s https://api.nuget.org/v3/index.json --ignore-failed-sources -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /ApiGateway


FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /ApiGateway

FROM base AS final
WORKDIR /ApiGateway
COPY --from=publish /ApiGateway .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]